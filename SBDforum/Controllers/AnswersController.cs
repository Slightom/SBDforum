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
    public class AnswersController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Answers
        public ActionResult Index()
        {
            var answers = db.Answers.Include(a => a.Thread).Include(a => a.User);
            return View(answers.ToList());
        }

        // GET: Answers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // GET: Answers/Create
        public ActionResult Create()
        {
            ViewBag.ThreadID = new SelectList(db.Threads, "ThreadID", "Title");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Nick");
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AnswerID,ThreadID,UserID,Date,Points,Text")] Answer answer)
        {
            if (ModelState.IsValid)
            {

                ViewBag.badWord = HomeController.consistBadWord(answer.Text);
                if (ViewBag.badWord != "")
                {
                    return View("BadWord");
                }

                db.Answers.Add(answer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ThreadID = new SelectList(db.Threads, "ThreadID", "Title", answer.ThreadID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Nick", answer.UserID);
            return View(answer);
        }








        // GET: Answers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            ViewBag.ThreadID = new SelectList(db.Threads, "ThreadID", "Title", answer.ThreadID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Nick", answer.UserID);
            return View(answer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AnswerID,ThreadID,UserID,Date,Points,Text")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ThreadID = new SelectList(db.Threads, "ThreadID", "Title", answer.ThreadID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Nick", answer.UserID);
            return View(answer);
        }









        // GET: Answers/Edit/5
        public ActionResult SpecialEdit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }

            ViewBag.ThreadID = new SelectList(db.Threads, "ThreadID", "Title", answer.ThreadID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Nick", answer.UserID);
            return View(answer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SpecialEdit([Bind(Include = "AnswerID,ThreadID,UserID,Date,Points,Text")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answer).State = EntityState.Modified;
                db.SaveChanges();
                int T = answer.ThreadID;
                return RedirectToAction("Thread", "Home", new { id = T });
            }
            ViewBag.ThreadID = new SelectList(db.Threads, "ThreadID", "Title", answer.ThreadID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Nick", answer.UserID);
            return View(answer);
        }








        // GET: Answers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Answer a = db.Answers.Find(id);
            var comments = db.Comments.Where(p => p.AnswerID == id);
            foreach (var c in comments)
            {
                db.Comments.Remove(c);              
            }
            db.SaveChanges();


            db.Answers.Remove(a);
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
