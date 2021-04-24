using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TryMoreWeb.Models;

namespace TryMoreWeb.Controllers
{
    public class RegisterController : Controller
    {
        public ActionResult Register(int type)
        {
            UsersModel obj = new UsersModel();

            Session.Clear();
            Session.Abandon();
            obj.Age = null;

            if (type == null || type == 0 || type > 2)
            {
                RedirectToAction("Login", "Register");
            }

            obj.RefType = type;
            ViewBag.UserType = type;
            return View(obj);

        }

        public ActionResult Login()
        {
            Session.Clear();
            Session.Abandon();
            UsersModel obj = new UsersModel();
            return View(obj);
        }


        [HttpPost]
        public string SetSession(UsersModel usersModel)
        {
            if (usersModel.UserTokenInfo!=null)
            {
                Session["access_token"] = usersModel.UserTokenInfo.access_token;
            }
            Session["FirstName"] = usersModel.FirstName;
            Session["LastName"] = usersModel.LastName;
            Session["Email"] = usersModel.Email;
            Session["UserType"] = usersModel.UserType;
            Session["ProfileImage"] = usersModel.ProfileImage;
            return "";
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }


    }
}