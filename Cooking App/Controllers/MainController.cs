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
                string Email = l.Email;
                string Password = l.Password;
                Login u = lmethods.GetName(l.Email, l.Password);
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
            u.Password = form["password"];
            u.PhotoName = "Profile.jpg";

            bool ans = lmethods.Save(u);
            if (ans)
            {
                return RedirectToAction("LoginPage");
            }
             
            return View();
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
    
        public ActionResult Profile()
        {
            Login u = new Login();
            Logged l = lmethods.TempName();
            u = lmethods.GetName(l.Name, l.Password);
            TempData["T1"] = u.UserName;
            int id = u.Id;

            Login p = lmethods.GetInfoProfile(id);
            ViewBag.ID = p.Id;
            ViewBag.Username = p.UserName;
            ViewBag.Email = p.Email;
            ViewBag.Password = p.Password;
            ViewBag.Gender = p.Gender;
            ViewBag.Profession = p.Profession;
            ViewBag.City = p.City;
            ViewBag.DOB = p.DOB;        // Date Of Birth
            ViewBag.PhotoName = p.PhotoName;
            return View();
        }
        [HttpPost]
        public ActionResult Profile(FormCollection form)
        {
            Login l = new Login();
            l.UserName = username;
            l.Email = email;
            l.Password = password;
            string profe = profession != null ? profession : " ";
            p.Profession = profe;
            string d1 = dob != null ? dob : " ";    // dob = Date Of Birth
            p.DOB = d1;
            string c1 = city != null ? city : " ";
            p.City = c1;
            string gen = gender != null ? gender : " ";
            p.Gender = gen;
            p.Id = id;
            if (pro.ProfilePhoto == null)
            {
                if (remove != null)
                {
                    p.PhotoName = "Profile.jpg";
                    int res = mdb.UpdateProfilePhoto(p);
                    if (res == 1)
                        return RedirectToAction("Profile");
                }
                else
                {
                    int res = mdb.UpdateProfile(p);
                    if (res == 1)
                        return RedirectToAction("Profile");
                }
            }
            else
            {
                p.PhotoName = uniqueFileName;
                int res = mdb.UpdateProfilePhoto(p);
                if (res == 1)
                    return RedirectToAction("Profile");
            }
            return View();
        }

    }
}