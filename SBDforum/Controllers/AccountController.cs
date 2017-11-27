using SBDforum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SBDforum.Controllers
{
    public class AccountController : Controller
    {
        private MyDbContext db = new MyDbContext();
        // GET: Account
        public ActionResult Index()
        {
            //var top3List = from a in db.Answer
            //               group a by a.Id_user into newGroup
            //               select newGroup.K;



            return View(db.Users.ToList());
        }




        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var usr = db.Users.FirstOrDefault(u => u.Nick == user.Nick && u.Password == user.Password && u.Active == true);
            if (usr != null)
            {
                Session["UserID"] = usr.UserID.ToString();
                Session["UserName"] = usr.Nick.ToString();

                if(usr.Role == true)
                {
                    Session["admin"] = "admin";
                }


                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Username or Password wrong");
                user.Nick = "";
                user.Password = "";
                return View();
            }
        }



        public ActionResult LogOut()
        {
            Session["UserID"] = null;
            Session["UserName"] = null;
            Session["admin"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}