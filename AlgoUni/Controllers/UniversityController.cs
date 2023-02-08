using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlgoUni.Models;
using System.Net;

namespace AlgoUni.Controllers
{
    public class UniversityController : Controller
    {
        UniversityRegister db = new UniversityRegister();
        // GET: University
        public ActionResult Login()
        {
            Session.Abandon();
            return View();
        }

        [HttpPost]
        public ActionResult Login(Membership membership, Model model)
        {


            var UserDetail = db.UniversityDetails.Where(x => x.EmailID == membership.EmailId && x.Password == membership.Password).FirstOrDefault();
            if (UserDetail == null)
            {

                return View("Login",membership);
            }
            else
            {
                Session["model"] = membership;


                return RedirectToAction("Index","CollegeDetails");
            }

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
            return RedirectToAction("Login", "CompanyRegister");
        }
    }
}