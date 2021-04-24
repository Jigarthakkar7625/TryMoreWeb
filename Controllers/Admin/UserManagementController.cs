using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TryMoreWeb.Models;

namespace TryMoreWeb.Controllers.Admin
{
    public class UserManagementController : Controller
    {
        // GET: Customers
        public ActionResult Customers()
        {

            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }

            UsersModel obj = new UsersModel();
            obj.UserType = 1;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiCollections.API_GET_ALL_USERS);
            request.Method = "POST";
            request.KeepAlive = true;
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("Authorization", "Bearer " + Session["access_token"]);


            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(obj);

                streamWriter.Write(json);
                streamWriter.Flush();
            }

            string myResponse = "";


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
            {
                myResponse = sr.ReadToEnd();
            }
            List<UsersModel> users = (new JavaScriptSerializer()).Deserialize<List<UsersModel>>(myResponse);

            return View(users);
        }

        // GET: Customers
        public ActionResult Sellers()
        {

            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }

            UsersModel obj = new UsersModel();
            obj.UserType = 2;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiCollections.API_GET_ALL_USERS);
            request.Method = "POST";
            request.KeepAlive = true;
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("Authorization", "Bearer " + Session["access_token"]);


            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(obj);

                streamWriter.Write(json);
                streamWriter.Flush();
            }

            string myResponse = "";


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
            {
                myResponse = sr.ReadToEnd();
            }
            List<UsersModel> users = (new JavaScriptSerializer()).Deserialize<List<UsersModel>>(myResponse);

            return View(users);
        }

        public ActionResult ChangePassword()
        {
            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiCollections.API_CHANGE_PASSWORD);
            request.Method = "POST";
            request.KeepAlive = true;
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("Authorization", "Bearer " + Session["access_token"]);
            //ChangePasswordViewModel obj = new ChangePasswordViewModel();
            //model.BookingID = Convert.ToInt32(BookingID);
            //model.Status = status;
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(model);

                streamWriter.Write(json);
                streamWriter.Flush();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string myResponse = "";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
            {
                myResponse = sr.ReadToEnd();
            }
            ResponseData m = (new JavaScriptSerializer()).Deserialize<ResponseData>(myResponse);
            TempData["success"] = m.success;
            TempData["message"] = m.message;

            return View(model);
        }


        public ActionResult UpdateProfile()
        {
            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiCollections.API_USER_PROFILE);
            request.Method = "GET";
            request.KeepAlive = true;
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("Authorization", "Bearer " + Session["access_token"]);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string myResponse = "";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
            {
                myResponse = sr.ReadToEnd();
            }

            UsersModel users = (new JavaScriptSerializer()).Deserialize<UsersModel>(myResponse);


            return View(users);
        }

        [HttpPost]
        public ActionResult UpdateProfile(UsersModel model)
        {
            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }

            if (model.files != null)
            {
                model.isFiles = true;
                model.FileExt =  Path.GetExtension(model.files.FileName);
            }
            else
            {
                model.isFiles = false;

            }

            HttpPostedFileBase httpPostedFileBase = model.files;

            model.files = null;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiCollections.API_USER_UPDTAEPROFILE);
            request.Method = "POSt";
            request.KeepAlive = true;
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("Authorization", "Bearer " + Session["access_token"]);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(model);

                streamWriter.Write(json);
                streamWriter.Flush();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string myResponse = "";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
            {
                myResponse = sr.ReadToEnd();
            }

            UsersModel users = (new JavaScriptSerializer()).Deserialize<UsersModel>(myResponse);

            if (httpPostedFileBase != null && users.ProfileImage != null && users.ProfileImage != "")
            {
                string currentDirectory = Path.GetDirectoryName(users.ProfileImage);

                string fullPathOnly = System.IO.Path.Combine(
                                       Server.MapPath("~" + currentDirectory));

                if (!Directory.Exists(fullPathOnly))
                {
                    Directory.CreateDirectory(fullPathOnly);
                }

                string filePath = System.IO.Path.Combine(
                                       Server.MapPath("~"+ users.ProfileImage));
                httpPostedFileBase.SaveAs(filePath);

            }

            return View(users);
        }
    }
}