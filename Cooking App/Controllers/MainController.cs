using Cooking_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult LoginPage(FormCollection form, DateTime date)
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
                int res = lmethods.Save(u);
                if (res == 1)
                    return RedirectToAction("LoginPage");
            }
            else if (date == null)
            {
                int res = lmethods.Search(u.Email, u.Password);
                if (res == 1)
                {
                    string Email = u.Email;
                    string Password = u.Password;
                    u = lmethods.GetName(u.Email, u.Password);
                    TempData["T1"] = u.UserName;
                    lmethods.Temporary(Email, Password);
                    TempData["sucess"] = "success";
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
                int res = lmethods.ForgetPassword(u.Email, date, u.Password);
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
        [HttpPost]
        public ActionResult StateWiseMenu(Receipe imag, string ingredient, string htm, string choice1, string choice2)
        {
            Receipe m = new Receipe();
            string uniqueFileName = null;
            //if (imag.Photo != null)
            //{
            //    uniqueFileName = Guid.NewGuid().ToString() + "_" + imag.Photo.FileName;
            //    string uploadsFolder = Path.Combine(Server.MapPath("~/Image"),uniqueFileName);
            //    imag.Photo.CopyTo(new FileStream(uploadsFolder, FileMode.Create));


            //}
            string youtube = imag.Youtube;
            youtube = youtube.Replace("watch?v=", "embed/");
            imag.Youtube = youtube;
            m.Youtube = youtube;
            m.RName = imag.RName;       // RName = Recipe Name
            m.HTM = imag.HTM;           // HTM = How to Make
            m.Ingredient = ingredient;
            m.Photo = uniqueFileName;
            m.State = choice2;
            m.VNB = choice1;            //VNB = Veg Non-Veg Beverage

            Login u = new Login();
            Logged l = lmethods.TempName();
            u = lmethods.GetRole(l.Name, l.Password);
            m.RoleId = u.RoleID;
            m.UserId = u.Id;
            int res = methods.Insert(m);
            if (res == 1)
                return RedirectToAction("StateWiseMenu");

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

        //[HttpPost]
        //public ActionResult MainMenu(OnlineFoodReceipe.Models.Img imag, string ingredient, string htm, string choice1, string choice2)
        //{
        //    Menu m = new Menu();
        //    string uniqueFileName = null;
        //    if (imag.Photo != null)
        //    {
        //        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "NewlyAddedImg");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + imag.Photo.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        imag.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
        //    }
        //    string youtube = imag.Youtube;
        //    youtube = youtube.Replace("watch?v=", "embed/");
        //    imag.Youtube = youtube;
        //    m.Youtube = youtube;
        //    m.RName = imag.RName;       //RName = Recipe Name
        //    m.HTM = imag.HTM;           //HTM = How To Make
        //    m.Ingredient = ingredient;
        //    m.Photo = uniqueFileName;
        //    m.State = choice2;
        //    m.VNB = choice1;            //VNB = Veg Non-Veg Beverage

        //    Login u = new Login();
        //    Logged l = db.TempName();
        //    u = db.GetRole(l.Name, l.Password);
        //    m.RoleId = u.RoleID;
        //    m.UserId = u.Id;
        //    int res = mdb.Insert(m);
        //    if (res == 1)
        //        return RedirectToAction("MainMenu");
        //    return View();
        //}


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
    }
}