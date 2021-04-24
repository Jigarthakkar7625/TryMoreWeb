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
    public class BrandController : Controller
    {
        public ActionResult GetBrands()
        {

            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiCollections.API_GetBrands);
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
            List<BrandModel> brandModelModels = (new JavaScriptSerializer()).Deserialize<List<BrandModel>>(myResponse);

            return View(brandModelModels);
        }


        public ActionResult Brand(int BrandID = 0)
        {
            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }


            if (BrandID > 0)
            {
                BrandModel brandModel = new BrandModel();
                brandModel.BrandID = BrandID;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiCollections.API_GetBrandId);
                request.Method = "POST";
                request.KeepAlive = true;
                request.ContentType = "application/json; charset=utf-8";
                request.Headers.Add("Authorization", "Bearer " + Session["access_token"]);

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(brandModel);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string myResponse = "";
                using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    myResponse = sr.ReadToEnd();
                }

                brandModel = (new JavaScriptSerializer()).Deserialize<BrandModel>(myResponse);

                return View(brandModel);
            }


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Brand(BrandModel model)
        {
            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiCollections.API_SaveBrand);
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

            return RedirectToAction("GetBrands", "Brand");

        }


    }
}