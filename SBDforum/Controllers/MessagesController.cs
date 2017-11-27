using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SBDforum.Models;

namespace SBDforum.Controllers
{
    public class MessagesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Messages
        public ActionResult Index()
        {
            var messages = db.Messages.Include(m => m.Receiver).Include(m => m.Sender);
            return View(messages.ToList());
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }










        // GET: Messages/Create
        public ActionResult Create()
        {
            ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "Nick");
            ViewBag.SenderID = new SelectList(db.Users, "UserID", "Nick");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MessageID,Date,SenderID,ReceiverID,Text")] Message message)
        {
            if (ModelState.IsValid)
            {

                ViewBag.badWord = HomeController.consistBadWord(message.Text);
                if (ViewBag.badWord != "")
                {
                    return View("BadWord");
                }

                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "Nick", message.ReceiverID);
            ViewBag.SenderID = new SelectList(db.Users, "UserID", "Nick", message.SenderID);
            return View(message);
        }





        // GET: Messages/Create
        public ActionResult SpecialCreate(int idR)
        {
            int idS = Int32.Parse(Session["UserID"].ToString());

            Message m = new Message();
            m.SenderID = idS;
            m.ReceiverID = idR;
            m.Receiver = db.Users.Find(idR);

            Session["ReceiverID"] = idR;

            ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "Nick");
            ViewBag.SenderID = new SelectList(db.Users, "UserID", "Nick");

            return View(m);
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SpecialCreate([Bind(Include = "MessageID,Date,SenderID,ReceiverID,Text")] Message message)
        {
            if (ModelState.IsValid)
            {

                ViewBag.badWord = HomeController.consistBadWord(message.Text);
                if (ViewBag.badWord != "")
                {
                    return View("BadWord");
                }

                message.Date = DateTime.Now;

                int s = Int32.Parse(Session["UserID"].ToString());
                int r = Int32.Parse(Session["ReceiverID"].ToString());

                message.SenderID = s;
                message.ReceiverID = r;
                db.Messages.Add(message);
                db.SaveChanges();

                if(Session["t"] != null)
                {
                    int t = Int32.Parse(Session["t"].ToString());
                    Session["t"] = null;
                    return RedirectToAction("Thread", "Home", new { id = t });
                    
                }
                else
                {
                    return RedirectToAction("Mymessages", "Home", new { id = message.SenderID });
                }

               
            }

            ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "Nick", message.ReceiverID);
            ViewBag.SenderID = new SelectList(db.Users, "UserID", "Nick", message.SenderID);
            return View(message);
        }
















        // GET: Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "Nick", message.ReceiverID);
            ViewBag.SenderID = new SelectList(db.Users, "UserID", "Nick", message.SenderID);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MessageID,Date,SenderID,ReceiverID,Text")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "Nick", message.ReceiverID);
            ViewBag.SenderID = new SelectList(db.Users, "UserID", "Nick", message.SenderID);
            return View(message);
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
