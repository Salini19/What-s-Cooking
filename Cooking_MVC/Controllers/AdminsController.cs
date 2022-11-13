using Cooking_MVC.Models;
using Cooking_WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace Cooking_MVC.Controllers
{
    public class AdminsController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44322/api/");
        //https://localhost:44322/
        HttpClient client;
        LoginMethods lmethods;
        public AdminsController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            lmethods = new LoginMethods();
        }
        public ActionResult Login()
        {
            TempData["M1"] = null;
            lmethods.DeleteLogged();
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {


            List<Admin> list = new List<Admin>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Admin").Result;
            if (response.IsSuccessStatusCode)
            {
                String Data = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<Admin>>(Data);
            }
            Admin l = new Admin();
            l.Email = form["email"];
            l.Password = form["password"];

            bool ans = list.Any(x => x.Email == l.Email && x.Password == l.Password);
            Admin u = list.FirstOrDefault(x => x.Email == l.Email && x.Password == l.Password);
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


        // GET: Admins
        public ActionResult Index()
        {
            List<Receipe> list = new List<Receipe>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Recipe").Result;
            if (response.IsSuccessStatusCode)
            {
                String Data = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<Receipe>>(Data);
            }
            return View(list);
            //Logged l = lmethods.TempName();
            //TempData["A1"] = l.Name;
            //if (TempData["A1"] != null)
            //{
            //    return View(list);
            //}
            //else
            //{
            //    return View("LoginPage");
            //}
            
        }

        // GET: Admins/Details/5
        public ActionResult Details(int id)
        {
           
            Logged l = lmethods.TempName();
            TempData["A1"] = l.Name;
            if (TempData["A1"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Receipe receipe = new Receipe();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Recipe/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    String Data = response.Content.ReadAsStringAsync().Result;
                    receipe = JsonConvert.DeserializeObject<Receipe>(Data);
                }
                if (receipe == null)
                {
                    return HttpNotFound();
                }
                return View(receipe);
            }
            return View();


        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            Logged l = lmethods.TempName();
            TempData["A1"] = l.Name;
            return View();
        }

        // POST: Admins/Create
        [HttpPost]
        public ActionResult Create(Img receipe)
        {
            string FileName = Path.GetFileNameWithoutExtension(receipe.ImageFile.FileName);

            string FileExtension = Path.GetExtension(receipe.ImageFile.FileName);

            FileName = FileName + DateTime.Now.ToString("yymmssfff") + FileExtension;
            receipe.Photo = "../RecipeImg/" + FileName;
            FileName = Path.Combine(Server.MapPath("../RecipeImg/"), FileName);


            receipe.ImageFile.SaveAs(FileName);

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


            string data = JsonConvert.SerializeObject(r);
           
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Receipe", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            else
            {
                return View();
            }
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admins/Edit/5
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

        // GET: Admins/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admins/Delete/5
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
