﻿using System;
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
        public ActionResult Login(Membership membership, AlgoUni.Models.UniversityDetail model)
        {


            var UserDetail = db.UniversityDetails.Where(x => x.EmailID == membership.EmailId && x.Password == membership.Password).FirstOrDefault();
            if (UserDetail == null)
            {

                return View("Login");
            }
            else
            {
                Session["univ2"] = UserDetail;


                return RedirectToAction("Index","StudentDetails");
            }

        }

        public ActionResult list()
        {
            var model = (AlgoUni.Models.UniversityDetail)Session["univ2"];
            ViewBag.mail = model.EmailID;
            ViewBag.pwd = model.Password;
            ViewBag.user = model.Username;
            ViewBag.code = model.UniversityCode;

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
    }
}