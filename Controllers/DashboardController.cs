﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TryMoreWeb.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Dashboard()
        {
            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}