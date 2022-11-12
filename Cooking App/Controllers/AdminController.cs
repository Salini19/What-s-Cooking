using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cooking_App.Models;

namespace Cooking_App.Controllers
{
    public class AdminController : Controller
    {
        private FoodReceipesEntities db = new FoodReceipesEntities();
        LoginMethods lmethods = new LoginMethods();

        public ActionResult LoginPage()
        {
            TempData["M1"] = null;
            lmethods.DeleteLogged();
            return View();
        }
        [HttpPost]
        public ActionResult LoginPage(FormCollection form)
        {
            Admin l = new Admin();
            l.Email = form["email"];
            l.Password = form["password"];

            bool ans =db.Admins.Any(x=>x.Email==l.Email && x.Password==l.Password);
            Admin u = db.Admins.FirstOrDefault(x=>x.Email==l.Email && x.Password==l.Password);
            if (ans)
            {            
                TempData["A1"] = u.Username;
                lmethods.Temporary(u.Username, u.Password);
                TempData["sucess"] = "success";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Invalid Username or password";
                return View();
            }

        }
        public ActionResult LogOut()
        {
            lmethods.DeleteLogged();
            return RedirectToAction("MainPage","Main");
        }

        // GET: Admin
        public ActionResult Index()
        {
            Logged l= lmethods.TempName();
            TempData["A1"] = l.Name;
            if (TempData["A1"] !=null)
            {
                return View(db.Receipes.ToList());
            }
            else
            {
                return View("LoginPage");
            }
            
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            Logged l = lmethods.TempName();
            TempData["A1"] = l.Name;
            if (TempData["A1"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Receipe receipe = db.Receipes.Find(id);
                if (receipe == null)
                {
                    return HttpNotFound();
                }
                return View(receipe);
            }
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            Logged l = lmethods.TempName();
            TempData["A1"] = l.Name;
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Img receipe)
        {
            string FileName = Path.GetFileNameWithoutExtension(receipe.ImageFile.FileName);
 
            string FileExtension = Path.GetExtension(receipe.ImageFile.FileName);
 
            FileName = FileName+ DateTime.Now.ToString("yymmssfff") + FileExtension;
            receipe.Photo = "../RecipeImg/" + FileName;
            FileName = Path.Combine(Server.MapPath("../RecipeImg/"), FileName);

            //Get Upload path from Web.Config file AppSettings.  
            

            //Its Create complete path to store in server.  
           

            //To copy and save file into server.  
            receipe.ImageFile.SaveAs(receipe.Photo);

            Receipe r = new Receipe();
            r.RName = receipe.RName;
            r.Photo = FileName;
            string youtube = receipe.Youtube;
            youtube = youtube.Replace("watch?v=", "embed/");
            r.Youtube = youtube;
            r.HTM = receipe.HTM;
            r.Ingredient = receipe.Ingredient;
            r.State = receipe.State;
            r.VNB = receipe.VNB;

            if (r!=null)
            {
                db.Receipes.Add(r);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
            
           
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            Logged l = lmethods.TempName();
            TempData["A1"] = l.Name;
            if (TempData["A1"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Receipe receipe = db.Receipes.Find(id);
                if (receipe == null)
                {
                    return HttpNotFound();
                }
                return View(receipe);
            }
            return View();

          
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RId,RName,Photo,Youtube,Ingredient,HTM,VNB,State")] Receipe receipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(receipe);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            Logged l = lmethods.TempName();
            TempData["A1"] = l.Name;
            if (TempData["A1"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Receipe receipe = db.Receipes.Find(id);
                if (receipe == null)
                {
                    return HttpNotFound();
                }
                return View(receipe);
            }
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Receipe receipe = db.Receipes.Find(id);
            db.Receipes.Remove(receipe);
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
