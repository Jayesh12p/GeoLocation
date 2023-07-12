using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using GeoLocations.Models;

namespace GeoLocations.Controllers
{
    
    public class GeolocationsController : Controller
    {
        private MyGeoLocationEntities4 db = new MyGeoLocationEntities4();


         

        // GET: Geolocations
        public ActionResult Index()
        {
            return View(db.Geolocations.ToList());
        }

        // GET: Geolocations/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Geolocation geolocation = db.Geolocations.Find(id);
            if (geolocation == null)
            {
                return HttpNotFound();
            }
            return View(geolocation);
        }

        // GET: Geolocations/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Geolocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Engineer,Latitude,Longitude,Accuracy,Equipments,EPhoto")] Geolocation geolocation , HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                
                geolocation.CurrentDate = DateTime.Now.Date;
                geolocation.CurrentTime = DateTime.Now.TimeOfDay;
                db.Geolocations.Add(geolocation);
                Console.WriteLine(geolocation.Latitude);
                db.SaveChanges();

                string filename = geolocation.Id.ToString(); ;
                string extension=Path.GetExtension(geolocation.EPhoto.FileName);
                filename = filename + extension;
                geolocation.Equipments = "~/Photos/"+filename;
                filename=Path.Combine(Server.MapPath("~/Photos/"), filename);
                geolocation.EPhoto.SaveAs(filename);
    
                db.SaveChanges();
                

                //geolocation.CurrentDate= DateTime.Now.Date;
                //geolocation.CurrentTime = DateTime.Now.TimeOfDay;
                //db.Geolocations.Add(geolocation);
                //db.SaveChanges();
                //geolocation.Equipments = Server.MapPath("~/Photos/"+geolocation.Id)+ ".png";
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            

            return View(geolocation);
        }

        // GET: Geolocations/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Geolocation geolocation = db.Geolocations.Find(id);
            if (geolocation == null)
            {
                return HttpNotFound();
            }
            return View(geolocation);
        }

        // POST: Geolocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Engineer,Latitude,Longitude,Accuracy,Equipments,CurrentDate,CurrentTime")] Geolocation geolocation)
        {
            if (ModelState.IsValid)
            {
                geolocation.CurrentDate = DateTime.Now.Date;
                geolocation.CurrentTime = DateTime.Now.TimeOfDay;
                db.Entry(geolocation).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(geolocation);
        }

        // GET: Geolocations/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Geolocation geolocation = db.Geolocations.Find(id);
            if (geolocation == null)
            {
                return HttpNotFound();
            }
            return View(geolocation);
        }

        // POST: Geolocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Geolocation geolocation = db.Geolocations.Find(id);
            db.Geolocations.Remove(geolocation);
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
