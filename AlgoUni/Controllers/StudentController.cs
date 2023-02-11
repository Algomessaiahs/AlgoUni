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
using Microsoft.Ajax.Utilities;

namespace AlgoUni.Controllers
{
    public class StudentController : Controller
    {
        private UniversityRegister db = new UniversityRegister();

        public ActionResult Login()
        {

            Session.Abandon();
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(StudentDetail users, string ReturnUrl)
        {
            var user = db.StudentDetails.Where(x => x.EmailID == users.EmailID && x.Mobile== users.Mobile).FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(users.EmailID, true);
                Session["student"] = user;
                if (ReturnUrl != null)
                {
                    return Redirect(ReturnUrl);
                }

                else
                {
                    return RedirectToAction("StudentDashBoard");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {

            Session.Abandon();
            return RedirectToAction("Login");
        }

        public ActionResult StudentDashBoard()
        {
            
            return View();
        }

        public ActionResult StudentNotifyIndex()
        {
            return View(db.StudentExamNotices.ToList());
        }

        public ActionResult StudentNotifyEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentExamNotice examNotice = db.StudentExamNotices.Find(id);
            if (examNotice == null)
            {
                return HttpNotFound();
            }
            return View(examNotice);
        }

        [HttpPost]
        public ActionResult StudentNotifyEdit( int id, StudentExamNotice examNotice,Subject subject)
        {
            //var session = (AlgoUni.Models.StudentDetail)Session["student"];

            //var data = db.StudentExamNotices.FirstOrDefault(x => x.StudentExamNoticeID == id);
            //if (data != null)
            //{
            //    //data.CollegeCode = session.CollegeCode;
            //    //data.TutionFees = examNotice.TutionFees;
            //    db.SaveChanges();
            //}
            var session = (AlgoUni.Models.StudentDetail)Session["student"];
            var data = db.StudentExamNotices.FirstOrDefault(x => x.StudentExamNoticeID == id);
            var data2 = db.Subjects.Count(x => (x.Department_Code == session.Dept_Code) && (x.Semester == session.Semester));
            if (data != null)
            {
                data.DeptartmentCode = session.Dept_Code;
                data.Department = session.Department;
                data.studentID = session.StudentID;
                data.TotalCurrentSubjects = data2;
                data.Semester = session.Semester;
                data.Subjects = (data2 * data.ExamFees).ToString();

                //examNotice.DeptartmentCode = data.DeptartmentCode;
                //examNotice.Department = data.Department;
                //examNotice.studentID = data.studentID;
                //examNotice.SubjectCode = data2.ToString();
                db.SaveChanges();
            }

            return RedirectToAction("StudentNotifyIndex");
        }

        public ActionResult StudentNotifyDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentExamNotice examNotice = db.StudentExamNotices.Find(id);
            if (examNotice == null)
            {
                return HttpNotFound();
            }
            return View(examNotice);

        }
    }
}
