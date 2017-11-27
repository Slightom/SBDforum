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
    public class ThreadsController : Controller
    {
        private MyDbContext db = new MyDbContext();
        private MyDbContext db2 = new MyDbContext();

        // GET: Threads
        public ActionResult Index()
        {
            var threads = db.Threads.Include(t => t.Category).Include(t => t.User);
            return View(threads.ToList());
        }

        // GET: Threads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Threads.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }









        // GET: Threads/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Nick");
            return View();
        }

        // POST: Threads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ThreadID,CategoryID,UserID,Title,Date")] Thread thread)
        {
            if (ModelState.IsValid)
            {

                ViewBag.badWord = HomeController.consistBadWord(thread.Title);
                if (ViewBag.badWord != "")
                {
                    return View("BadWord");
                }

                db.Threads.Add(thread);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", thread.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Nick", thread.UserID);
            return View(thread);
        }






        // GET: Threads/Create
        public ActionResult SpecialCreate()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Nick");
            return View();
        }

        // POST: Threads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SpecialCreate([Bind(Include = "ThreadID,CategoryID,Title")] Thread thread)
        {
            if (ModelState.IsValid)
            {

                ViewBag.badWord = HomeController.consistBadWord(thread.Title);
                if (ViewBag.badWord != "")
                {
                    return View("BadWord");
                }

                db.Threads.Add(thread);
                db.SaveChanges();
                return RedirectToAction("Index");


                thread.Date = DateTime.Now;
                int UID = Int32.Parse(Session["UserID"].ToString());
                thread.UserID = UID;

                db.Threads.Add(thread);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", thread.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Nick", thread.UserID);
            return View(thread);
        }

















        // GET: Threads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Threads.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", thread.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Nick", thread.UserID);
            return View(thread);
        }

        // POST: Threads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ThreadID,CategoryID,UserID,Title,Date")] Thread thread)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thread).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", thread.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Nick", thread.UserID);
            return View(thread);
        }






        // GET: Threads/Edit/5
        public ActionResult SpecialEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Threads.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", thread.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Nick", thread.UserID);
            return View(thread);
        }

        // POST: Threads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SpecialEdit([Bind(Include = "ThreadID,CategoryID,UserID,Title,Date")] Thread thread)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thread).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Thread", "Home", new { id = thread.ThreadID });
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", thread.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Nick", thread.UserID);
            return View(thread);
        }








        // GET: Threads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Threads.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        // POST: Threads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Thread t = db.Threads.Find(id);

            var answers = db.Answers.Where(p => p.ThreadID == id);
            foreach (var a in answers)
            {
                var comments = db2.Comments.Where(p => p.AnswerID == a.AnswerID);
                foreach (var c in comments)
                {
                    db2.Comments.Remove(c);                    
                }
                db2.SaveChanges();
                db.Answers.Remove(a);               
            }
            db.SaveChanges();

            db.Threads.Remove(t);
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
