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
    public class BannedWordsDictionariesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: BannedWordsDictionaries
        public ActionResult Index()
        {
            return View(db.BannedWordsDictionaries.ToList());
        }

        // GET: BannedWordsDictionaries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BannedWordsDictionary bannedWordsDictionary = db.BannedWordsDictionaries.Find(id);
            if (bannedWordsDictionary == null)
            {
                return HttpNotFound();
            }
            return View(bannedWordsDictionary);
        }

        // GET: BannedWordsDictionaries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BannedWordsDictionaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BannedWordsDictionaryID,Name")] BannedWordsDictionary bannedWordsDictionary)
        {
            if (ModelState.IsValid)
            {
                db.BannedWordsDictionaries.Add(bannedWordsDictionary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bannedWordsDictionary);
        }

        // GET: BannedWordsDictionaries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BannedWordsDictionary bannedWordsDictionary = db.BannedWordsDictionaries.Find(id);
            if (bannedWordsDictionary == null)
            {
                return HttpNotFound();
            }
            return View(bannedWordsDictionary);
        }

        // POST: BannedWordsDictionaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BannedWordsDictionaryID,Name")] BannedWordsDictionary bannedWordsDictionary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bannedWordsDictionary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bannedWordsDictionary);
        }

        // GET: BannedWordsDictionaries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BannedWordsDictionary bannedWordsDictionary = db.BannedWordsDictionaries.Find(id);
            if (bannedWordsDictionary == null)
            {
                return HttpNotFound();
            }
            return View(bannedWordsDictionary);
        }

        // POST: BannedWordsDictionaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BannedWordsDictionary bannedWordsDictionary = db.BannedWordsDictionaries.Find(id);
            db.BannedWordsDictionaries.Remove(bannedWordsDictionary);
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
