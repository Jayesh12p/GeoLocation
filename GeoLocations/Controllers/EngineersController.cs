using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GeoLocations.Models;
using EntityState = System.Data.Entity.EntityState;

namespace GeoLocations.Controllers
{
    public class EngineersController : Controller
    {
        private MyGeoLocationEntities4 db = new MyGeoLocationEntities4();

        // GET: Engineers
        public ActionResult Index()
        {
            return View(db.Engineers.ToList());
        }

        // GET: Engineers/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Engineer engineer = db.Engineers.Find(id);
            if (engineer == null)
            {
                return HttpNotFound();
            }
            return View(engineer);
        }

        // GET: Engineers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Engineers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Email,Password,Name")] Engineer engineer)
        {
            if (ModelState.IsValid)
            {
                db.Engineers.Add(engineer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(engineer);
        }

        // GET: Engineers/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Engineer engineer = db.Engineers.Find(id);
            if (engineer == null)
            {
                return HttpNotFound();
            }
            return View(engineer);
        }

        // POST: Engineers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Email,Password,Name")] Engineer engineer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(engineer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(engineer);
        }

        // GET: Engineers/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Engineer engineer = db.Engineers.Find(id);
            if (engineer == null)
            {
                return HttpNotFound();
            }
            return View(engineer);
        }

        // POST: Engineers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Engineer engineer = db.Engineers.Find(id);
            db.Engineers.Remove(engineer);
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
