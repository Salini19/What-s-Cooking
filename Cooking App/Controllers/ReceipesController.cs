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
    public class ReceipesController : Controller
    {
        private FoodReceipesEntities db = new FoodReceipesEntities();

        // GET: Receipes
        public ActionResult Index()
        {
            var receipes = db.Receipes.Include(r => r.Login).Include(r => r.State1);
            return View(receipes.ToList());
        }

        // GET: Receipes/Details/5
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

        // GET: Receipes/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Logins, "Id", "UserName");
            ViewBag.State = new SelectList(db.States, "Sname", "Sname");
            return View();
        }

        // POST: Receipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RId,RName,Photo,Youtube,Ingredient,HTM,VNB,RoleId,State,UserId")] Receipe receipe)
        {
            if (ModelState.IsValid)
            {
                db.Receipes.Add(receipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Logins, "Id", "UserName", receipe.UserId);
            ViewBag.State = new SelectList(db.States, "Sname", "Sname", receipe.State);
            return View(receipe);
        }

        // GET: Receipes/Edit/5
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
            ViewBag.UserId = new SelectList(db.Logins, "Id", "UserName", receipe.UserId);
            ViewBag.State = new SelectList(db.States, "Sname", "Sname", receipe.State);
            return View(receipe);
        }

        // POST: Receipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RId,RName,Photo,Youtube,Ingredient,HTM,VNB,RoleId,State,UserId")] Receipe receipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Logins, "Id", "UserName", receipe.UserId);
            ViewBag.State = new SelectList(db.States, "Sname", "Sname", receipe.State);
            return View(receipe);
        }

        // GET: Receipes/Delete/5
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

        // POST: Receipes/Delete/5
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
