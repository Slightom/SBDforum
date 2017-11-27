using SBDforum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace SBDforum.Controllers
{
    public class HomeController : Controller
    {
        private MyDbContext db = new MyDbContext();
        private MyDbContext db2 = new MyDbContext();

        public class Top // klasa reprezentująca top-Użytkownika
        {
            public string Nick { get; set; }
            public int Points { get; set; }
            public string Level { get; set; }
        }


        public class AnswersComments
        {
            public int AnswerID { get; set; }
            public ICollection<Comment> Comments { get; set; }
        }



        public ActionResult Index(string searchString, int? page, int? CID)
        {


            var threads = db.Threads.Select(n => n);// wybieram wszystkie wątki

            if (!String.IsNullOrEmpty(searchString)) // jeśli jest wyszukiwanie, to wybierz odpowiednie
            {
                ViewBag.searchString = searchString;
                threads = threads.Where(s => s.Title.Contains(searchString));
            }

            if (CID != null)
            {
                string category = db.Categories.Where(p => p.CategoryID == CID).Select(n => n.Name).FirstOrDefault().ToString();
                ViewBag.searchCategory = category;
                threads = threads.Where(s => s.CategoryID == CID);
            }

            threads = threads.OrderByDescending(d => d.Date);



            var query4 = db.Answers.GroupBy(a => new { a.UserID, a.User.Nick })  // wybiera 3 najlepszych użytkowników
                           .OrderByDescending(g => g.Sum(p => p.Points))
                           .Select(g => new
                           {
                               Nick = g.Key.Nick,
                               Points = g.Sum(p => p.Points)
                           })
                           .Take(3);


            List<Top> top3 = new List<Top>();

            foreach (var g in query4)
            {
                Top top = new Top();
                top.Nick = g.Nick;
                top.Points = (int)g.Points;
                top.Level = db2.IntervalPoints.Where(x => g.Points >= x.LowerRange && g.Points <= x.UpperRange)
                                                .Select(n => n.Name).FirstOrDefault();
                top3.Add(top);
            }
            ViewBag.top3 = top3;


            return View(threads.ToList().ToPagedList(page ?? 1, 3));
        }

        public ActionResult Rules()
        {
            return View(db.Rules.ToList());
        }


        public ActionResult Thread(int id)
        {

            Thread t = db.Threads.Where(x => x.ThreadID == id).FirstOrDefault();

            var query = db.Answers.Where(p => p.ThreadID == id).Select(n => n).OrderByDescending(p => p.Points).ToList();
            ViewBag.answers = query;


            return View(t);
        }




        //public ActionResult SetVariable(string key, string value)
        //{
        //    Session[key] = value;

        //    return this.Json(new { success = true });
        //}



        public PartialViewResult OrderedAnswersByDate(int id)
        {
            var query = db.Answers.Where(p => p.ThreadID == id).Select(n => n).OrderByDescending(p => p.Date).ToList();

            return PartialView("OrderedAnswers", query.ToList());
        }


        public PartialViewResult OrderedAnswersByPoints(int id)
        {
            var query = db.Answers.Where(p => p.ThreadID == id).Select(n => n).OrderByDescending(p => p.Points).ToList();

            return PartialView("OrderedAnswers", query.ToList());
        }


        public ActionResult Categories()
        {
            var query = db.Categories.Select(n => n).ToList();

            return View(query);
        }

        public ActionResult CategoriesShow(int? id)
        {
            var threads = new List<Thread>();

            if (id != null)
            {
                threads = db.Threads.Where(p => p.CategoryID == id).Select(n => n).OrderByDescending(d => d.Date).ToList();// wybieram wszystkie wątki z danej kategorii
            }
            else
            {
                threads = db.Threads.Where(p => p.CategoryID == 1).Select(n => n).OrderByDescending(d => d.Date).ToList();// wybieram wszystkie wątki z danej kategorii
            }




            var query4 = db.Answers.GroupBy(a => new { a.UserID, a.User.Nick })  // wybiera 3 najlepszych użytkowników
                           .OrderByDescending(g => g.Sum(p => p.Points))
                           .Select(g => new
                           {
                               Nick = g.Key.Nick,
                               Points = g.Sum(p => p.Points)
                           })
                           .Take(3);


            List<Top> top3 = new List<Top>();

            foreach (var g in query4)
            {
                Top top = new Top();
                top.Nick = g.Nick;
                top.Points = (int)g.Points;
                top.Level = db2.IntervalPoints.Where(x => g.Points >= x.LowerRange && g.Points <= x.UpperRange)
                                                .Select(n => n.Name).FirstOrDefault();
                top3.Add(top);
            }
            ViewBag.top3 = top3;


            return View("Index", threads);
        }



        public string VoteAdd(string vname)
        {
            int aID = IdOfAnswer(vname);
            var a = db.Answers.Where(p => p.AnswerID == aID).Select(n => n).FirstOrDefault();

            int uID = Int32.Parse(Session["UserID"].ToString());
            var app = HttpContext.Application;
            string nazwa = "vote" + aID + "_" + uID;
            if (app[nazwa] == null || app[nazwa] == "O")
            {
                app[nazwa] = "P";
                a.Points += 1;

                db.SaveChanges();
                return "O";
            }
            else if (app[nazwa] == "M")
            {
                app[nazwa] = "O";
                a.Points += 1;

                db.SaveChanges();
                return "O";
            }
            else
            {
                return "P";
            }


        }

        public string VoteSub(string vname)
        {
            int aID = IdOfAnswer(vname);
            var a = db.Answers.Where(p => p.AnswerID == aID).Select(n => n).FirstOrDefault();

            int uID = Int32.Parse(Session["UserID"].ToString());
            var app = HttpContext.Application;
            string nazwa = "vote" + aID + "_" + uID;

            if (app[nazwa] == null || app[nazwa] == "O")
            {
                app[nazwa] = "M";
                a.Points -= 1;

                db.SaveChanges();
                return "O";
            }
            else if (app[nazwa] == "P")
            {
                app[nazwa] = "O";
                a.Points -= 1;

                db.SaveChanges();
                return "O";
            }
            else
            {
                return "M";
            }
        }

        private int IdOfAnswer(string x)
        {
            return Int32.Parse(x.Substring(5));
        }

        public string SessionAttributeExists(string x)
        {
            if (Session[x] != null)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }







        [HttpPost]
        public ActionResult AddComment(string comment, string aID)
        {
            int AID = Int32.Parse(aID);
            string TID = db.Answers.Where(p => p.AnswerID == AID).Select(p => p.ThreadID).FirstOrDefault().ToString();
            int tID = Int32.Parse(TID);
            int uID = Int32.Parse(Session["UserID"].ToString());

            ViewBag.badWord = consistBadWord(comment);
            if (ViewBag.badWord != "")
            {
                return View("BadWord");
            }


            Comment c = new Comment();
            c.UserID = uID;
            c.AnswerID = AID;
            c.Text = comment;
            c.Date = DateTime.Now;
            db.Comments.Add(c);
            db.SaveChanges();

            return RedirectToAction("Thread", new { id = tID });
        }


        public ActionResult DeleteComment(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("DeleteComment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCommentConfirmed(int id)
        {
            //int CID = Int32.Parse(cID);
            Comment c = db.Comments.Find(id);

            int tID = c.Answer.Thread.ThreadID;

            db.Comments.Remove(c);
            db.SaveChanges();

            return RedirectToAction("Thread", new { id = tID });
        }











        [HttpPost]
        public ActionResult AddAnswer(string answer, string tID)
        {
            int TID = Int32.Parse(tID);
            int UID = Int32.Parse(Session["UserID"].ToString());


            ViewBag.badWord = consistBadWord(answer);
            if (ViewBag.badWord != "")
            {
                return View("BadWord");
            }

            Answer a = new Answer();
            a.UserID = UID;
            a.ThreadID = TID;
            a.Text = answer;
            a.Date = DateTime.Now;
            a.Points = 0;
            db.Answers.Add(a);
            db.SaveChanges();

            return RedirectToAction("Thread", new { id = TID });
        }


        public ActionResult DeleteAnswer(int id)
        {
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }


        // POST: Answers/Delete/5
        [HttpPost, ActionName("DeleteAnswer")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAnswerConfirmed(int id)
        {
            //int CID = Int32.Parse(cID);
            Answer a = db.Answers.Find(id);
            var comments = db.Comments.Where(p => p.AnswerID == id);
            foreach (var c in comments)
            {
                db.Comments.Remove(c);
            }
            db.SaveChanges();

            int tID = a.ThreadID;

            db.Answers.Remove(a);
            db.SaveChanges();

            return RedirectToAction("Thread", new { id = tID });
        }




        public ActionResult DeleteThread(int id)
        {
            Thread thread = db.Threads.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }


        // POST: Threads/Delete/5
        [HttpPost, ActionName("DeleteThread")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteThreadConfirmed(int id)
        {
            //int CID = Int32.Parse(cID);
            Thread t = db.Threads.Find(id);

            var answers = db.Answers.Where(p => p.ThreadID == id);
            foreach (var a in answers)
            {
                var comments = db2.Comments.Where(p => p.AnswerID == a.AnswerID);
                foreach (var c in comments)
                {
                    db.Comments.Remove(c);

                }
                db.Answers.Remove(a);
                db.SaveChanges();
            }
            db.SaveChanges();

            db.Threads.Remove(t);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }










        public static string consistBadWord(string comment)
        {
            MyDbContext dbx = new MyDbContext();
            List<string> badwords = dbx.BannedWordsDictionaries.Select(n => n.Name).ToList();

            foreach (string bw in badwords)
            {
                if (comment.Contains(bw))
                {
                    return bw;
                }
            }

            return "";
        }



        public ActionResult MyConto()
        {
            string ids = Session["UserID"].ToString();
            int id = Int32.Parse(ids);

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            int points;
            if (user.Answers.Count() == 0)
            {
                points = 0;
            }
            else
            {
                points = db.Answers.Where(p => p.UserID == id).Sum(p => p.Points);
            }

            ViewBag.Points = points;

            return View(user);
        }


        // GET: Users/Edit/5
        public ActionResult EditCount(int id)
        {

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCount([Bind(Include = "UserID,Nick,Email,Password,Role,Avatar,Active")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyConto");
            }
            return View(user);
        }






        public ActionResult UploadPhoto(int id)
        {

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        //////////////////// controler

        [HttpPost]
        public ActionResult UploadPhoto(User user, HttpPostedFileBase upload)
        {
            //rozszerzenie pliku
            var ext = Path.GetExtension(upload.FileName);
            // if (ext != ".jpg") return new HttpStatusCodeResult(401);

            //pelny sciezka do pliku
            var path = Path.Combine(Server.MapPath("~/Content/photos"), upload.FileName);
            //zapis
            upload.SaveAs(path);


            string ids = Session["UserID"].ToString();
            int id = Int32.Parse(ids);


            User u = new User();
            u = db.Users.Find(id);
            u.Avatar = "/Content/photos/" + upload.FileName;
            db.SaveChanges();

            return RedirectToAction("MyConto");
        }







        public ActionResult MyMessages(int? id)
        {


            return View();
        }


        public PartialViewResult sendReceivedMessages()
        {
            int id = Int32.Parse(Session["UserID"].ToString());

            var messagesReceived = db.Messages.Where(p => p.ReceiverID == id).OrderByDescending(p => p.Date).ToList();
            var messagesSend = db.Messages.Where(p => p.SenderID == id).OrderByDescending(p => p.Date).ToList();

            ViewBag.messagesSend = messagesSend;

            return PartialView("sendReceivedMessages", messagesReceived);
        }

        public string readBefore(int id)
        {
            Message m = db.Messages.Where(p => p.MessageID == id).FirstOrDefault();

            if (m.Read == false)
            {
                m.Read = true;
                db.SaveChanges();
                return "false";
            }

            return "true";
        }


        public int howManyNms()
        {
            if (Session["UserID"] != null)
            {
                int id = Int32.Parse(Session["UserID"].ToString());
                int ile = db.Messages.Where(p => p.ReceiverID == id && p.Read == false).Count();

                return ile;
            }

            return -1;
        }

        public static int howManyNms2(string xid)
        {
            MyDbContext db = new MyDbContext();
            int id = Int32.Parse(xid);
            int ile = db.Messages.Where(p => p.ReceiverID == id && p.Read == false).Count();

            return ile;
        }

    }
}