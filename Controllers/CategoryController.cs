using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TryMoreWeb.Models;

namespace TryMoreWeb.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult GetCategories()
        {

            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiCollections.API_GetCategories);
            request.Method = "GET";
            request.KeepAlive = true;
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("Authorization", "Bearer " + Session["access_token"]);
            string myResponse = "";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
            {
                myResponse = sr.ReadToEnd();
            }
            List<CategoryModel> leadCategoryModels = (new JavaScriptSerializer()).Deserialize<List<CategoryModel>>(myResponse);

            return View(leadCategoryModels);
        }


        public ActionResult Category(int CategoryID = 0)
        {
            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }


            if (CategoryID > 0)
            {
                CategoryModel categoryModel = new CategoryModel();
                categoryModel.CategoryID = CategoryID;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiCollections.API_GetCategoryId);
                request.Method = "POST";
                request.KeepAlive = true;
                request.ContentType = "application/json; charset=utf-8";
                request.Headers.Add("Authorization", "Bearer " + Session["access_token"]);

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(categoryModel);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string myResponse = "";
                using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    myResponse = sr.ReadToEnd();
                }

                categoryModel = (new JavaScriptSerializer()).Deserialize<CategoryModel>(myResponse);

                return View(categoryModel);
            }


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Category(CategoryModel model)
        {
            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiCollections.API_SaveCategory);
            request.Method = "POST";
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
            ResponseData m = (new JavaScriptSerializer()).Deserialize<ResponseData>(myResponse);
            TempData["success"] = m.success;
            TempData["message"] = m.message;
            if (m.success == false)
            {
                return View(model);
            }

            return RedirectToAction("GetCategories", "Category");

        }


    }
}