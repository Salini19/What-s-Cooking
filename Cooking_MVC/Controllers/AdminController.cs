using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cooking_MVC.Models;

namespace Cooking_MVC.Controllers
{
    public class AdminController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44322/api/");
       //https://localhost:44358/
        HttpClient client;
        public AdminController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public ActionResult Login()
        {
            TempData["M1"] = null;
            lmethods.DeleteLogged();
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            
            
            List<Admin> l = new List<Admin>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Admin").Result;
            if (response.IsSuccessStatusCode)
            {
                String Data = response.Content.ReadAsStringAsync().Result;
                l = JsonConvert.DeserializeObject<List<Admin>>(Data);
            }
            Admin l = new Admin();
            l.Email = form["email"];
            l.Password = form["password"];

            bool ans = db.Admins.Any(x => x.Email == l.Email && x.Password == l.Password);
            Admin u = db.Admins.FirstOrDefault(x => x.Email == l.Email && x.Password == l.Password);
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


            return View();
        }
















            return View();
        }
        
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
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

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
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

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
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
