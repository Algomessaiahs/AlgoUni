using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlgoUni.Models;
using System.Net;
using System.Web.Security;
using System.Data.Entity;
using AlgoUni.ViewModel;
using System.Runtime.InteropServices;

namespace AlgoUni.Controllers
{
    public class UniversityController : Controller
    {
        private UniversityRegister db = new UniversityRegister();
        //TechathonDB_user11Entities db = new TechathonDB_user11Entities();
        // GET: University
        public ActionResult Login()
        {

            Session.Abandon();
            return View("Login");
        }

        [HttpPost]
        
        public ActionResult Login(UniversityDetail users, string ReturnUrl)
        {
            var user = db.UniversityDetails.Where(x => x.EmailID == users.EmailID && x.Password == users.Password).FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(users.EmailID, true);
                Session["univ2"] = user;
                if (ReturnUrl != null)
                {
                    return Redirect(ReturnUrl);
                }

                 else
                {
                    return RedirectToAction("Dashboard");
                }
            }
            return View();
        }

       
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UniversityDetail data)
        {
            ViewBag.Message = "University";

            {
                db.UniversityDetails.Add(data);
                db.SaveChanges();
            }
            return RedirectToAction("Login");

        }

        public ActionResult Logout()
        {

            Session.Abandon();
            return RedirectToAction("Login");
        }

        public ActionResult Dashboard()
        {
            return View();
        }

       
        public ActionResult College()
        {
            return RedirectToAction("CollegeIndex");

        }

        // GET: CollegeDetails
        public ActionResult CollegeIndex()
        {
            return View(db.CollegeDetails.ToList());
        }

        // GET: CollegeDetails/Details/5
        public ActionResult CollegeDetails(int? id)
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
        [Authorize(Roles = "Admin")]
        // GET: CollegeDetails/Create
        public ActionResult CollegeCreate()
        {
            return View();
        }

        // POST: CollegeDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult CollegeCreate([Bind(Include = "CollegeID,CollegeCode,CollegeName,City,Username,EmailID,Password,UniversityCode")] CollegeDetail collegeDetail)
        {
            var session = (AlgoUni.Models.UniversityDetail)Session["univ2"];
            var data = session.UniversityCode;
            collegeDetail.UniversityCode = data;
            
            if (ModelState.IsValid)
            {
                db.CollegeDetails.Add(collegeDetail);
                db.SaveChanges();
                return RedirectToAction("CollegeIndex");
            }

            return View(collegeDetail);
        }
        //[Authorize(Roles ="AO")]
        // GET: CollegeDetails/Edit/5
        public ActionResult CollegeEdit(int? id)
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
        //[Authorize(Roles = "AO")]
        public ActionResult CollegeEdit([Bind(Include = "CollegeID,CollegeCode,CollegeName,City,Username,EmailID,Password,UniversityCode")] CollegeDetail collegeDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collegeDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CollegeIndex");
            }
            return View(collegeDetail);
        }

        // GET: CollegeDetails/Delete/5
        public ActionResult CollegeDelete(int? id)
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
        [HttpPost, ActionName("CollegeDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult CollegeDeleteConfirmed(int id)
        {
            CollegeDetail collegeDetail = db.CollegeDetails.Find(id);
            db.CollegeDetails.Remove(collegeDetail);
            db.SaveChanges();
            return RedirectToAction("CollegeIndex");
        }

        public ActionResult ExamNotify()
        {
            return RedirectToAction("ExamNotifyIndex");
        }
        public ActionResult ExamNotifyIndex()
        {
            return View(db.ExamNotifications.ToList());
        }

        // GET: ExamNotifications/Details/5
        public ActionResult ExamNotifyDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamNotification examNotification = db.ExamNotifications.Find(id);
            if (examNotification == null)
            {
                return HttpNotFound();
            }
            return View(examNotification);
        }

        // GET: ExamNotifications/Create
        public ActionResult ExamNotifyCreate()
        {
            return View();
        }

        // POST: ExamNotifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExamNotifyCreate([Bind(Include = "ExamNotifyID,ExamCode,Degree,Semester,ExamFees,OtherFees,UnivCode")] ExamNotification examNotification)
        {
            var session = (AlgoUni.Models.UniversityDetail)Session["univ2"];
            var data = session.UniversityCode;
            examNotification.UnivCode = data;
            if (ModelState.IsValid)
            {
                db.ExamNotifications.Add(examNotification);
                db.SaveChanges();
                return RedirectToAction("ExamNotifyIndex");
            }

            return View(examNotification);
        }

        // GET: ExamNotifications/Edit/5
        public ActionResult ExamNotifyEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamNotification examNotification = db.ExamNotifications.Find(id);
            if (examNotification == null)
            {
                return HttpNotFound();
            }
            return View(examNotification);
        }

        // POST: ExamNotifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExamNotifyEdit([Bind(Include = "ExamNotifyID,ExamCode,Degree,Semester,ExamFees,OtherFees,UnivCode")] ExamNotification examNotification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examNotification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ExamNotifyIndex");
            }
            return View(examNotification);
        }

        // GET: ExamNotifications/Delete/5
        public ActionResult ExamNotifyDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamNotification examNotification = db.ExamNotifications.Find(id);
            if (examNotification == null)
            {
                return HttpNotFound();
            }
            return View(examNotification);
        }

        // POST: ExamNotifications/Delete/5
        [HttpPost, ActionName("ExamNotifyDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ExamNotifyDeleteConfirmed(int id)
        {
            ExamNotification examNotification = db.ExamNotifications.Find(id);
            db.ExamNotifications.Remove(examNotification);
            db.SaveChanges();
            return RedirectToAction("ExamNotifyIndex");
        }

        public ActionResult ExamNotifySend(ExamNotification notify,CollegeExamNotice collegeExamNotice,int id)
        {
            var data = db.ExamNotifications.FirstOrDefault(x => x.ExamNotifyID == id);
            if (data != null)
            {
                collegeExamNotice.ExamCode = data.ExamCode;
                collegeExamNotice.Degree = data.Degree;
                collegeExamNotice.Semester = data.Semester;
                collegeExamNotice.ExamFees = data.ExamFees;
                collegeExamNotice.OtherFees = data.OtherFees;
                collegeExamNotice.UnivCode = data.UnivCode;
                db.CollegeExamNotices.Add(collegeExamNotice);
                db.SaveChanges();
            }
           return RedirectToAction("ExamNotifyIndex");
        }
public ActionResult Result()
        {
            return RedirectToAction("ResultIndex");
        }         // GET: Results
        public ActionResult ResultIndex()
        {
            MasterViewModel obj = new MasterViewModel(); obj.SubjectCode = (from objects in db.Subjects
                                                                            select new SelectListItem()
                                                                            {
                                                                                Text = objects.SubjectCode,
                                                                                Value = objects.SubjectCode
                                                                            }
            ).ToList();
            obj.Subject = (from objects in db.Subjects
                           select new SelectListItem()
                           {
                               Text = objects.Subjects,
                               Value = objects.Subjects
                           }
            ).ToList(); return View(obj);
        }
        [HttpPost]
        public ActionResult ResultIndex(StudentViewModel objstud, Result result)
        {
            result.StudentID = objstud.studentID;
            result.DepartmentCode = Convert.ToInt32(objstud.DepartmentCode);
            result.Department = objstud.Department; db.Results.Add(result);
            db.SaveChanges(); foreach (var item in objstud.ListstudentMarksViewModels)
            {
                MARK data2 = new MARK()
                {
                    StudentID = objstud.studentID,
                    SubjectCode = item.SubjectCode,
                    Subject = item.Subject,
                    Grade = item.Grade
                };
                db.MARKS.Add(data2);
                db.SaveChanges();
            }
            return Json(data: new { message = "Data Successfully Added.", status = true }, JsonRequestBehavior.AllowGet);
        }


        // GET: Results/Details/5
        public ActionResult ResultDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: Results/Create
        public ActionResult ResultCreate()
        {
            return View();
        }

        // POST: Results/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResultCreate([Bind(Include = "ResultID,ExamCode,Degree,StudentID,StudentName,Department,DepartmentCode,Semester,Subjects,ExamResult,Grade,CollegeCode,UnivCode")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Results.Add(result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(result);
        }

        // GET: Results/Edit/5
        public ActionResult ResultEdit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Result result = db.Results.Find(id);
            //var data = db.Subjects.Select(x => x.Semester == result.Semester && x.Department_Code == result.DepartmentCode).ToList();
            //Subject subject = new Subject();
            //subject.SubjectCode = (for sub in db.Subjects
            //    select new subject()
            //{

            //}).ToList();
            //using (var context = new UniversityRegister())
            //{
            //    ViewBag.res = (from SubjectCode in context.Subjects
            //               join
            //               subject in context.Results on SubjectCode.Semester equals subject.Semester
            //               where subject.Department == SubjectCode.Department
            //               select subject.Subjects).ToList();

            //    return View(ViewBag.res);
            //}

            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResultEdit([Bind(Include = "ResultID,ExamCode,Degree,StudentID,StudentName,Department,DepartmentCode,Semester,Subjects,ExamResult,Grade,CollegeCode,UnivCode")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(result);
        }

        // GET: Results/Delete/5
        public ActionResult ResultDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ResultDeleteConfirmed(int id)
        {
            Result result = db.Results.Find(id);
            db.Results.Remove(result);
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

        