using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlgoUni.Models;

namespace AlgoUni.Controllers
{
    public class CollegeDetailsController : Controller
    {
        private UniversityRegister db = new UniversityRegister();

        // GET: CollegeDetails
        public ActionResult Index()
        {
            return View(db.CollegeDetails.ToList());
        }

        // GET: CollegeDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollegeDetail collegeDetail = db.CollegeDetails.Find(id);
            if (collegeDetail == null)
            {
                return HttpNotFound();
            }
            return View(collegeDetail);
        }

        // GET: CollegeDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CollegeDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CollegeID,CollegeCode,CollegeName,City,Username,EmailID,Password,UniversityCode")] CollegeDetail collegeDetail)
        {
            if (ModelState.IsValid)
            {
                db.CollegeDetails.Add(collegeDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(collegeDetail);
        }

        // GET: CollegeDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollegeDetail collegeDetail = db.CollegeDetails.Find(id);
            if (collegeDetail == null)
            {
                return HttpNotFound();
            }
            return View(collegeDetail);
        }

        // POST: CollegeDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CollegeID,CollegeCode,CollegeName,City,Username,EmailID,Password,UniversityCode")] CollegeDetail collegeDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collegeDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(collegeDetail);
        }

        // GET: CollegeDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollegeDetail collegeDetail = db.CollegeDetails.Find(id);
            if (collegeDetail == null)
            {
                return HttpNotFound();
            }
            return View(collegeDetail);
        }

        // POST: CollegeDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CollegeDetail collegeDetail = db.CollegeDetails.Find(id);
            db.CollegeDetails.Remove(collegeDetail);
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
