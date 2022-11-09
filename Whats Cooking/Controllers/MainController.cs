using DAL_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Whats_Cooking.Controllers
{
    public class MainController : Controller
    {
        ReceipeMethods methods = null;
        LoginMethods lmethods = null;
        public MainController()
        {
            methods = new ReceipeMethods();
            lmethods = new LoginMethods();
        }


        public ActionResult MainPage()
        {

            ViewBag.Id = methods.GetCountId();
            ViewBag.Rid = methods.GetCountRId();
            TempData["T1"] = null;
            TempData["M1"] = "MainPage";
            lmethods.DeleteLogged();
            ViewBag.FeedList = db.FeedbackInfo();
            return View();


        }
        [HttpPost]
        public ActionResult MainPage(string name, string email, string msg)
        {
            int res = db.Feedback(name, email, msg);
            ViewBag.FeedList = lmethods.FeedbackInfo();
            return View();
            
        }

        
        public ActionResult FeedbackPage()
        {
            ViewBag.FeedList = lmethods.AllFeedbackInfo();
            return View();
        }

       
        public ActionResult LoginPage()
        {
            TempData["M1"] = null;
            db.DeleteLogged();
            return View();
        }
        [HttpPost]
        public ActionResult LoginPage(FormCollection form, string date)
        {
            Login u = new Login();
            string fname = form["fname"];
            string lname = form["lname"];
            string FullName = string.Empty;
            if (fname != null)
            {
                FullName = fname + " " + lname;
            }
            else
                FullName = fname;
            u.UserName = FullName;
            u.Email = form["email"];
            u.Password = form["password"];
            if (u.UserName != null && date == null)
            {
                u.RoleID = 2;
                u.PhotoName = "Profile.jpg";
                int res = db.Save(u);
                if (res == 1)
                    return RedirectToAction("LoginPage");
            }
            else if (date == null)
            {
                int res = db.Search(u.Email, u.Password);
                if (res == 1)
                {
                    Email = u.Email;
                    Password = u.Password;
                    u = db.GetName(u.Email, u.Password);
                    TempData["T1"] = u.UserName;
                    db.Temporary(Email, Password);
                    TempData["sucess"] = "sucess";
                    return RedirectToAction("VNBMenu");
                }
                else
                {
                    ViewBag.Message = "Invalid Username or password";
                    return View();
                }
            }
            else
            {
                int res = db.ForgetPassword(u.Email, date, u.Password);
                if (res == 1)
                {
                    ViewBag.Msg1 = "Password Changed Sucessfully";
                    return View();
                }
                else
                {
                    ViewBag.Msg2 = "Given input is not valid";
                    return View();
                }
            }

            return View();
        }

        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        // GET: Main/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Main/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Main/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Main/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Main/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Main/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Main/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
