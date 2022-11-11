using Cooking_App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Security;


namespace Cooking_App.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
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
            ViewBag.FeedList = lmethods.FeedbackInfo();
            return View();


        }
        [HttpPost]
        public ActionResult MainPage(string name, string email, string msg)
        {
            int res = lmethods.Feedback(name, email, msg);
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
            lmethods.DeleteLogged();
            return View();
        }
        [HttpPost]
        public ActionResult LoginPage( FormCollection form)
        {
            Login l = new Login();
            l.Email = form["email"];
            l.Password = form["password"];

            bool ans = lmethods.Search(l.Email, l.Password);
            if (ans)
            {
                //string Email = l.Email;
                //string Password = l.Password;
                Login u = lmethods.GetName(l.Email, l.Password);
                TempData["T1"] = u.UserName;
                lmethods.Temporary(l.Email, l.Password);
                TempData["sucess"] = "success";
                return RedirectToAction("VNBMenu");
            }
            else
            {
                ViewBag.Message = "Invalid Username or password";
                return View();
            }
       
        }

        public ActionResult SignupPage()
        {
            TempData["M1"] = null;
            lmethods.DeleteLogged();
            return View();
        }


        [HttpPost]
        public ActionResult SignupPage(FormCollection form)
        {
            Login u = new Login();
            string fname = form["fname"];
            string lname = form["lname"];
            string FullName = string.Empty;
            if (fname != null)
            {
                FullName = fname + " " + lname;
            }

            FullName = fname;
            u.UserName = FullName;
            u.Email = form["email"];
            u.DOB = Convert.ToDateTime(form["dob"]);
            u.MobileNumber = form["no"];
            u.Password = form["password"];
            string s = form["confirm password"];
            u.PhotoName = "Profile.jpg";
            if (s == u.Password)
            {
                bool ans = lmethods.Save(u);
                if (ans)
                {
                    return RedirectToAction("LoginPage");
                }


            }
            else
            {
                ViewBag.pass = "Confirm password and password should be same";
                return View();
            }


            return View();
        }
        public ActionResult ForgotPassword()
        {
            TempData["M1"] = null;
            lmethods.DeleteLogged();
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(FormCollection form)
        {
            Login l = new Login();
            l.Email = form["email"];
            l.DOB = Convert.ToDateTime(form["date"]);
            l.Password = form["password"];

            int res = lmethods.ForgetPassword(l.Email, (DateTime)l.DOB, l.Password);
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

        public ActionResult LogOut()
        {
            lmethods.DeleteLogged();
            return RedirectToAction("MainPage");
        }

        public ActionResult VNBMenu()
        {

            Login u = new Login();
            Logged l = lmethods.TempName();
            u = lmethods.GetName(l.Name, l.Password);
            TempData["T1"] = u.UserName;
            return View();

        }

        public ActionResult StateWiseMenu(string id)
        {
            Login u = new Login();
            Logged l = lmethods.TempName();
            u = lmethods.GetName(l.Name, l.Password);
            TempData["T1"] = u.UserName;
            ViewBag.Id = u.Id;
            lmethods.Temporaryvnb(l.Name, id);

            //State m = new State();
            ViewBag.StateList = methods.GetAllState(id);
            return View();
        }


        public ActionResult MainMenu(string id)
        {
            Login u = new Login();
            Logged l = lmethods.TempName();
            u = lmethods.GetName(l.Name, l.Password);
            TempData["T1"] = u.UserName;
            lmethods.Temporarystate(l.Name, id);
            TempData["sname"] = l.Sname;

            Receipe m = new Receipe();
            ViewBag.Id = u.Id;
            ViewBag.Image = m.Photo;
            ViewBag.Vnb = l.Vnb;            //vnb = Veg Non-Veg Beverage
            ViewBag.List = methods.GetAllProducts(id, l.Vnb);
            return View();
        }

        public ActionResult Info(int id)
        {
            Login u = new Login();
            Logged l = lmethods.TempName();
            u = lmethods.GetName(l.Name, l.Password);
            TempData["T1"] = u.UserName;

            Receipe m = methods.GetInfo(id);
            ViewBag.Rname = m.RName;   
            ViewBag.Vnb = m.VNB;        
            ViewBag.State = m.State;
            ViewBag.Photo = m.Photo;
            ViewBag.Youtube = m.Youtube;
            ViewBag.Ingredient = m.Ingredient;
            ViewBag.Htm = m.HTM;      
            return View();
        }


        public ActionResult Delete(int id)
        {
            int res = methods.Delete(id);
            if (res == 1)
                return RedirectToAction("Modify");
            return View();
        }

        // Delete Beverage Action

        public ActionResult DeleteBeverage(int id)
        {
            int res = methods.Delete(id);
            if (res == 1)
                return RedirectToAction("ModifyBeverage");
            return View();
        }
    
        public ActionResult Profile()
        {
            Login u = new Login();
            Logged l = lmethods.TempName();
            u = lmethods.GetName(l.Name, l.Password);
            TempData["T1"] = u.UserName;
            int id = u.Id;

            Login p = lmethods.GetInfoProfile(id);
          
            return View(p);
        }
        [HttpPost]
        public ActionResult Profile(Login l)
        {
        
            bool ans= lmethods.UpdateProfile(l);
            if (ans)
            {
                ViewBag.profile = "Profile Updated Successfully";
                return RedirectToAction("VNBMenu");
                
            }
             
           
            return View();
        }

       
    }
}