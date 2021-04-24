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
    public class SMSPlanController : Controller
    {
        public ActionResult GetAllSMSPlan()
        {

            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiCollections.API_SMSPlan);
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
            List<SMSPlanModel> sMSPlanModels = (new JavaScriptSerializer()).Deserialize<List<SMSPlanModel>>(myResponse);

            return View(sMSPlanModels);
        }


        public ActionResult SMSPlan(int SMSPlanID = 0)
        {
            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }
            if (SMSPlanID > 0)
            {
                SMSPlanModel sMSPlanModel = new SMSPlanModel();
                sMSPlanModel.SMSPlanID = SMSPlanID;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiCollections.API_GetSMSPlanByID);
                request.Method = "POST";
                request.KeepAlive = true;
                request.ContentType = "application/json; charset=utf-8";
                request.Headers.Add("Authorization", "Bearer " + Session["access_token"]);
                //ChangePasswordViewModel obj = new ChangePasswordViewModel();
                //model.BookingID = Convert.ToInt32(BookingID);
                //model.Status = status;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(sMSPlanModel);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string myResponse = "";
                using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    myResponse = sr.ReadToEnd();
                }

                sMSPlanModel = (new JavaScriptSerializer()).Deserialize<SMSPlanModel>(myResponse);

                sMSPlanModel.IsActiveBool = sMSPlanModel.IsActive == 1 ? true : false;
                return View(sMSPlanModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SMSPlan(SMSPlanModel model)
        {
            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }

            model.IsActive = model.IsActiveBool ? 1 : 0;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiCollections.API_SaveSMSPlan);
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

            if (m.success == false)
            {
                return View(model);
            }

            return RedirectToAction("GetAllSMSPlan", "SMSPlan");

        }
    }
}