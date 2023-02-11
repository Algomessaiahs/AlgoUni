using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AlgoUni.Models;

namespace AlgoUni.Controllers
{
    public class CollegeController : Controller
    {
        private UniversityRegister db = new UniversityRegister();

        public ActionResult Login()
        {

            Session.Abandon();
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(CollegeDetail users, string ReturnUrl)
        {
            var user = db.CollegeDetails.Where(x => x.EmailID == users.EmailID && x.Password == users.Password).FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(users.EmailID, true);
                Session["College"] = user;
                if (ReturnUrl != null)
                {
                    return Redirect(ReturnUrl);
                }

                else
                {
                    return RedirectToAction("CollegeDashBoard");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {

            Session.Abandon();
            return RedirectToAction("Login");
        }

        public ActionResult CollegeDashBoard()
        {
            return View();
        }

        public ActionResult Student()
        {
            return RedirectToAction("Index");
        }

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
        public ActionResult Create([Bind(Include = "STUD_ID,RegisterNumber,StudentID,StudentName,Semester,Degree,Dept_Code,Department,EmailID,Mobile,DateofBirth,JoiningYear,CompletionYear,CollegeCode,UniversityCode")] StudentDetail studentDetail)
        {
            studentDetail.StudentID = studentDetail.JoiningYear.Substring(2) + studentDetail.Department + db.StudentDetails.Count().ToString("000");
            studentDetail.RegisterNumber = Convert.ToInt64(studentDetail.CollegeCode + studentDetail.JoiningYear.Substring(2) + studentDetail.Dept_Code + db.StudentDetails.Count().ToString("00"));
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
        public ActionResult Edit([Bind(Include = "STUD_ID,RegisterNumber,StudentID,StudentName,Semester,Degree,Dept_Code,Department,EmailID,Mobile,DateofBirth,JoiningYear,CompletionYear,CollegeCode,UniversityCode")] StudentDetail studentDetail)
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

        public ActionResult CollegeNotifyIndex()
        {
            return View(db.CollegeExamNotices.ToList());
        }

        public ActionResult CollegeNotifyEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollegeExamNotice examNotice = db.CollegeExamNotices.Find(id);
            if (examNotice == null)
            {
                return HttpNotFound();
            }
            return View(examNotice);
        }

        [HttpPost]
        public ActionResult CollegeNotifyEdit(CollegeExamNotice examNotice,int id,StudentExamNotice studentExamNotice)
        {
            var session = (AlgoUni.Models.CollegeDetail)Session["college"];
          
            var data = db.CollegeExamNotices.FirstOrDefault(x => x.CollegeExamNoticeID == id);
            if (data != null)
            {
                data.CollegeCode = session.CollegeCode;
                data.TutionFees = examNotice.TutionFees;
                db.SaveChanges();
            }

            return RedirectToAction("CollegeNotifyIndex");
        }

        public ActionResult CollegeNotifyDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollegeExamNotice examNotice = db.CollegeExamNotices.Find(id);
            if (examNotice == null)
            {
                return HttpNotFound();
            }
            return View(examNotice);
            
        }

        public ActionResult StudentNotifySend( int id,StudentExamNotice studentExamNotice)
        {
            var data = db.CollegeExamNotices.FirstOrDefault(x => x.CollegeExamNoticeID == id);
            if (data != null)
            {
                studentExamNotice.ExamCode = data.ExamCode;
                studentExamNotice.Degree = data.Degree;
                //studentExamNotice.Semester = data.Semester;
                studentExamNotice.ExamFees = data.ExamFees;
                studentExamNotice.OtherFees = data.OtherFees;
                studentExamNotice.TutionFees = data.TutionFees;
                studentExamNotice.CollegeCode = data.CollegeCode;
                studentExamNotice.UnivCode = data.UnivCode;
                db.StudentExamNotices.Add(studentExamNotice);
                db.SaveChanges();
            }
            return RedirectToAction("CollegeNotifyIndex");
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
