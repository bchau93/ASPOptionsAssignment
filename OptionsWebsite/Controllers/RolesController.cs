using DiplomaDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace OptionsWebsite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Roles
        public ActionResult Index()
        {
           
            return View(db.Roles.ToList());
        }


        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, Name, Users")] IdentityRole role)
        {
            if(role.Name == null)
            {
                ModelState.AddModelError("Name", "The role name cannot be empty!");
                return View(role);
            }
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
           
        }
        

        public ActionResult AddUserToRole(string id)
        {
            List<SelectListItem> studentIds = new List<SelectListItem>();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            var result = db.Users.OrderBy(c => c.Id);
            foreach(IdentityUser user in db.Users.OrderBy(c => c.Id))
            {
                studentIds.Add(new SelectListItem
                {
                    Text = user.UserName,
                    Value = user.Id
                });
            }
            ViewBag.StudentId = studentIds.ToList();
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserToRole(string id, string uid)
        {
            
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            IdentityRole role = db.Roles.Find(id);
            ApplicationUser user = 
                db.Users
                .Where(u => u.Id.Equals(uid, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault();
            if(user != null)
            {
                userManager.AddToRole(user.Id, role.Name);
            }
            

            return RedirectToAction("Index");
        }

        public ActionResult RemoveUser(string id)
        {
            List<SelectListItem> studentIds = new List<SelectListItem>();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            var roleProvider = System.Web.Security.Roles.Provider;
            foreach (IdentityUser user in db.Users.OrderBy(c => c.Id).Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)))
            {
                studentIds.Add(new SelectListItem
                {
                    Text = user.UserName,
                    Value = user.Id
                });
            }
            ViewBag.StudentId = studentIds.ToList();
            return View(role);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveUser(string id, string uid)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            IdentityRole role = db.Roles.Find(id);
            ApplicationUser user =
                db.Users
                .Where(u => u.Id.Equals(uid, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault();
            if (user != null)
            {
                userManager.RemoveFromRole(user.Id, role.Name);
            }


            return RedirectToAction("Index");
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Users")] IdentityRole role)
        {
            if (role.Name == null)
            {
                ModelState.AddModelError("Name", "The role name cannot be empty!");
                return View(role);
            }
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            IdentityRole role  = db.Roles.Find(id);
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            foreach (IdentityUser user in db.Users.OrderBy(c => c.Id).Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)))
            {
                userManager.RemoveFromRole(user.Id, role.Id);
                    
            }
            db.Roles.Remove(role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
