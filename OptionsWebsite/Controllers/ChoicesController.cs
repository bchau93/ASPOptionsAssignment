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
    public class ChoicesController : Controller
    {
        private OptionsContext db = new OptionsContext();

        // GET: Choices
        //[Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            if (Request.IsAuthenticated && Roles.IsUserInRole("Admin"))
            {
                var choices = db.Choices.Include(c => c.FirstChoiceOption).Include(c => c.FourthChoiceOption).Include(c => c.SecondChoiceOption).Include(c => c.ThirdChoiceOption).Include(c => c.YearTerm);
                return View(choices.ToList());
            }
            else
            {
                return View("Error");
            }
   
        }

        // GET: Choices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        // GET: Choices/Create
        [Authorize(Roles = "Admin, Student")]
        public ActionResult Create()
        {
            ViewBag.FirstChoiceOptionId = new SelectList(db.Options.Where(c => c.isActive == true), "OptionsId", "Title");
            ViewBag.FourthChoiceOptionId = new SelectList(db.Options.Where(c => c.isActive == true), "OptionsId", "Title");
            ViewBag.SecondChoiceOptionId = new SelectList(db.Options.Where(c => c.isActive == true), "OptionsId", "Title");
            ViewBag.ThirdChoiceOptionId = new SelectList(db.Options.Where(c => c.isActive == true), "OptionsId", "Title");
           // ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId");
            return View();
        }

        // POST: Choices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin, Student")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChoiceId,YearTermId,StudentId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId,SelectionDate")] Choice choice)
        {
            var defaultTermId = db.YearTerms.Where(c => c.isDefault == true).First().YearTermId; 
            var modelState = db.Choices.Where(c => c.StudentId == choice.StudentId);
            if(modelState.Where(c => c.YearTermId == defaultTermId).Count() != 0)
            {
                ViewBag.FirstChoiceOptionId = new SelectList(db.Options.Where(c => c.isActive == true), "OptionsId", "Title", choice.FirstChoiceOptionId);
                ViewBag.FourthChoiceOptionId = new SelectList(db.Options.Where(c => c.isActive == true), "OptionsId", "Title", choice.FourthChoiceOptionId);
                ViewBag.SecondChoiceOptionId = new SelectList(db.Options.Where(c => c.isActive == true), "OptionsId", "Title", choice.SecondChoiceOptionId);
                ViewBag.ThirdChoiceOptionId = new SelectList(db.Options.Where(c => c.isActive == true), "OptionsId", "Title", choice.ThirdChoiceOptionId);

                return View(choice);
            }
            else
            {
                if (choice.FirstChoiceOptionId == choice.SecondChoiceOptionId
                   || choice.FirstChoiceOptionId == choice.ThirdChoiceOptionId
                   || choice.FirstChoiceOptionId == choice.FourthChoiceOptionId
                   || choice.SecondChoiceOptionId == choice.ThirdChoiceOptionId
                   || choice.SecondChoiceOptionId == choice.FourthChoiceOptionId
                   || choice.ThirdChoiceOptionId == choice.FourthChoiceOptionId)
                {
                    ViewBag.FirstChoiceOptionId = new SelectList(db.Options.Where(c => c.isActive == true), "OptionsId", "Title", choice.FirstChoiceOptionId);
                    ViewBag.FourthChoiceOptionId = new SelectList(db.Options.Where(c => c.isActive == true), "OptionsId", "Title", choice.FourthChoiceOptionId);
                    ViewBag.SecondChoiceOptionId = new SelectList(db.Options.Where(c => c.isActive == true), "OptionsId", "Title", choice.SecondChoiceOptionId);
                    ViewBag.ThirdChoiceOptionId = new SelectList(db.Options.Where(c => c.isActive == true), "OptionsId", "Title", choice.ThirdChoiceOptionId);

                    return View(choice);
                }
                else
                {
                    choice.YearTermId = defaultTermId;
                    choice.SelectionDate = DateTime.Now;
                    db.Choices.Add(choice);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                
            }

        }
        //[Authorize(Roles = "Admin")]
        // GET: Choices/Edit/5
        public ActionResult Edit(int? id)
        {

            if (Request.IsAuthenticated && Roles.IsUserInRole("Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Choice choice = db.Choices.Find(id);
                if (choice == null)
                {
                    return HttpNotFound();
                }
                ViewBag.FirstChoiceOptionId = new SelectList(db.Options, "OptionsId", "Title", choice.FirstChoiceOptionId);
                ViewBag.FourthChoiceOptionId = new SelectList(db.Options, "OptionsId", "Title", choice.FourthChoiceOptionId);
                ViewBag.SecondChoiceOptionId = new SelectList(db.Options, "OptionsId", "Title", choice.SecondChoiceOptionId);
                ViewBag.ThirdChoiceOptionId = new SelectList(db.Options, "OptionsId", "Title", choice.ThirdChoiceOptionId);
                ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId", choice.YearTermId);
                return View(choice);
            }
            else
            {
                return View("Error");
            }
           
        }

        // POST: Choices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChoiceId,YearTermId,StudentId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId,SelectionDate")] Choice choice)
        {

            if(db.YearTerms.Where(c => c.isDefault).Count() != 0)
            {
                choice.YearTermId = db.YearTerms.Where(c => c.isDefault == true).First().YearTermId;
            }
           

            if (Request.IsAuthenticated && Roles.IsUserInRole("Admin"))
            {
                if (ModelState.IsValid)
                {
                    if (choice.FirstChoiceOptionId == choice.SecondChoiceOptionId
                       || choice.FirstChoiceOptionId == choice.ThirdChoiceOptionId
                       || choice.FirstChoiceOptionId == choice.FourthChoiceOptionId
                       || choice.SecondChoiceOptionId == choice.ThirdChoiceOptionId
                       || choice.SecondChoiceOptionId == choice.FourthChoiceOptionId
                       || choice.ThirdChoiceOptionId == choice.FourthChoiceOptionId)
                    {
                        ViewBag.FirstChoiceOptionId = new SelectList(db.Options.Where(c => c.isActive == true), "OptionsId", "Title", choice.FirstChoiceOptionId);
                        ViewBag.FourthChoiceOptionId = new SelectList(db.Options.Where(c => c.isActive == true), "OptionsId", "Title", choice.FourthChoiceOptionId);
                        ViewBag.SecondChoiceOptionId = new SelectList(db.Options.Where(c => c.isActive == true), "OptionsId", "Title", choice.SecondChoiceOptionId);
                        ViewBag.ThirdChoiceOptionId = new SelectList(db.Options.Where(c => c.isActive == true), "OptionsId", "Title", choice.ThirdChoiceOptionId);

                        return View(choice);
                    }
                    //choice.YearTermId = defaultTermId;
                    choice.SelectionDate = DateTime.Now;
                    db.Entry(choice).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.FirstChoiceOptionId = new SelectList(db.Options, "OptionsId", "Title", choice.FirstChoiceOptionId);
                ViewBag.FourthChoiceOptionId = new SelectList(db.Options, "OptionsId", "Title", choice.FourthChoiceOptionId);
                ViewBag.SecondChoiceOptionId = new SelectList(db.Options, "OptionsId", "Title", choice.SecondChoiceOptionId);
                ViewBag.ThirdChoiceOptionId = new SelectList(db.Options, "OptionsId", "Title", choice.ThirdChoiceOptionId);
               // ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId", choice.YearTermId);
                return View(choice);
            }
            else
            {
                return View("Error");
            }

           
        }

        // GET: Choices/Delete/5
        //[Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (Request.IsAuthenticated && Roles.IsUserInRole("Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Choice choice = db.Choices.Find(id);
                if (choice == null)
                {
                    return HttpNotFound();
                }
                return View(choice);
            }
            else
            {
                return View("Error");
            }
           
        }

        // POST: Choices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Choice choice = db.Choices.Find(id);
            db.Choices.Remove(choice);
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
