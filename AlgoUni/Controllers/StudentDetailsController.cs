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
    public class StudentDetailsController : Controller
    {
        private UniversityRegister db = new UniversityRegister();

        // GET: StudentDetails
        public ActionResult Index()
        {
            return View(db.StudentDetails.ToList());
        }

        // GET: StudentDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentDetail studentDetail = db.StudentDetails.Find(id);
            if (studentDetail == null)
            {
                return HttpNotFound();
            }
            return View(studentDetail);
        }

        // GET: StudentDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "STUD_ID,RegisterNumber,StudentID,StudentName,Semester,Dept_Code,Department,EmailID,Mobile,DateofBirth,JoiningYear,CompletionYear,CollegeCode,UniversityCode")] StudentDetail studentDetail)
        {
            studentDetail.StudentID = studentDetail.JoiningYear.Substring(2) + studentDetail.Department + db.StudentDetails.Count();
            //studentDetail.RegisterNumber =studentDetail.CollegeCode + studentDetail.JoiningYear + db.StudentDetails.Count();
            studentDetail.RegisterNumber = Convert.ToInt32(studentDetail.CollegeCode) + Convert.ToInt32(studentDetail.JoiningYear.Substring(2)) + db.StudentDetails.Count();
            if (ModelState.IsValid)
            {
                db.StudentDetails.Add(studentDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studentDetail);
        }

        // GET: StudentDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentDetail studentDetail = db.StudentDetails.Find(id);
            if (studentDetail == null)
            {
                return HttpNotFound();
            }
            return View(studentDetail);
        }

        // POST: StudentDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "STUD_ID,RegisterNumber,StudentID,StudentName,Semester,Dept_Code,Department,EmailID,Mobile,DateofBirth,JoiningYear,CompletionYear,CollegeCode,UniversityCode")] StudentDetail studentDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentDetail);
        }

        // GET: StudentDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentDetail studentDetail = db.StudentDetails.Find(id);
            if (studentDetail == null)
            {
                return HttpNotFound();
            }
            return View(studentDetail);
        }

        // POST: StudentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentDetail studentDetail = db.StudentDetails.Find(id);
            db.StudentDetails.Remove(studentDetail);
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
