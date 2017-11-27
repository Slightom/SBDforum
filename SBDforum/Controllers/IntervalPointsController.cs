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
    public class IntervalPointsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: IntervalPoints
        public ActionResult Index()
        {
            return View(db.IntervalPoints.ToList());
        }

        // GET: IntervalPoints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntervalPoint intervalPoint = db.IntervalPoints.Find(id);
            if (intervalPoint == null)
            {
                return HttpNotFound();
            }
            return View(intervalPoint);
        }

        // GET: IntervalPoints/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IntervalPoints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IntervalPointID,LowerRange,UpperRange,Name")] IntervalPoint intervalPoint)
        {
            if (ModelState.IsValid)
            {
                db.IntervalPoints.Add(intervalPoint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(intervalPoint);
        }

        // GET: IntervalPoints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntervalPoint intervalPoint = db.IntervalPoints.Find(id);
            if (intervalPoint == null)
            {
                return HttpNotFound();
            }
            return View(intervalPoint);
        }

        // POST: IntervalPoints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IntervalPointID,LowerRange,UpperRange,Name")] IntervalPoint intervalPoint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(intervalPoint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(intervalPoint);
        }

        // GET: IntervalPoints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntervalPoint intervalPoint = db.IntervalPoints.Find(id);
            if (intervalPoint == null)
            {
                return HttpNotFound();
            }
            return View(intervalPoint);
        }

        // POST: IntervalPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IntervalPoint intervalPoint = db.IntervalPoints.Find(id);
            db.IntervalPoints.Remove(intervalPoint);
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
