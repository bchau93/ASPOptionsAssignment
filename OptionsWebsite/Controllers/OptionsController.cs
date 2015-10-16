using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomaDataModel;
using System.Web.Security;

namespace OptionsWebsite.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class OptionsController : Controller
    {
        private OptionsContext db = new OptionsContext();

        // GET: Options
        public ActionResult Index()
        {

            if (Request.IsAuthenticated && Roles.IsUserInRole("Admin"))
            {
                return View(db.Options.ToList());
            }
            else
            {
                return View("Error");
            }          
        }

        // GET: Options/Details/5
        public ActionResult Details(int? id)
        {
            if (Request.IsAuthenticated && Roles.IsUserInRole("Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Options options = db.Options.Find(id);
                if (options == null)
                {
                    return HttpNotFound();
                }
                return View(options);
            }
            else
            {
                return View("Error");
            }
           
        }

        // GET: Options/Create
        public ActionResult Create()
        {
            if (Request.IsAuthenticated && Roles.IsUserInRole("Admin"))
            {
               return View();
            }
            else
            {
                return View("Error");
            }
            
        }

        // POST: Options/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OptionsId,Title,isActive")] Options options)
        {

            if (Request.IsAuthenticated && Roles.IsUserInRole("Admin"))
            {
                if (ModelState.IsValid)
                {
                    db.Options.Add(options);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(options);
            }
            else
            {
                return View("Error");
            }
            
        }

        // GET: Options/Edit/5
        public ActionResult Edit(int? id)
        {

            if (Request.IsAuthenticated && Roles.IsUserInRole("Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Options options = db.Options.Find(id);
                if (options == null)
                {
                    return HttpNotFound();
                }
                return View(options);
            }
            else
            {
                return View("Error");
            }
            
        }

        // POST: Options/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OptionsId,Title,isActive")] Options options)
        {

            if (Request.IsAuthenticated && Roles.IsUserInRole("Admin"))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(options).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(options);
            }
            else
            {
                return View("Error");
            }
           
        }

        // GET: Options/Delete/5
        public ActionResult Delete(int? id)
        {

            if (Request.IsAuthenticated && Roles.IsUserInRole("Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Options options = db.Options.Find(id);
                if (options == null)
                {
                    return HttpNotFound();
                }
                return View(options);
            }
            else
            {
                return View("Error");
            }
           
        }

        // POST: Options/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            if (Request.IsAuthenticated && Roles.IsUserInRole("Admin"))
            {
                Options options = db.Options.Find(id);
                db.Options.Remove(options);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
            
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
