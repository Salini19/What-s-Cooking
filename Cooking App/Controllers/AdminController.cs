using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.Receipes.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
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

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RId,RName,Photo,Youtube,Ingredient,HTM,VNB,State")] Receipe receipe)
        {
            if (ModelState.IsValid)
            {
                db.Receipes.Add(receipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(receipe);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
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
