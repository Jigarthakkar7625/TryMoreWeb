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

namespace TryMoreWeb.Controllers.Admin
{
    public class LeadChargesController : Controller
    {
        public ActionResult GetAllLeadCharges()
        {

            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiCollections.API_GetLeadCharges);
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
            List<LeadChargeModel> leadChargeModels = (new JavaScriptSerializer()).Deserialize<List<LeadChargeModel>>(myResponse);


            foreach (var item in leadChargeModels)
            {
                item.LeadProductAmountName = GetEnumName(item.LeadProductAmountID);
            }

            

            return View(leadChargeModels);
        }

        public string GetEnumName(int ID) {
            EnumLeadCharges testEnum = (EnumLeadCharges)ID;
            var fieldInfo = testEnum.GetType().GetMember(testEnum.GetType().GetEnumName(testEnum));
            var descriptionAttribute = fieldInfo[0]
                       .GetCustomAttributes(typeof(DescriptionAttribute), false)
                       .FirstOrDefault() as DescriptionAttribute;

            return descriptionAttribute.Description.ToString();
        }

        public ActionResult LeadCharges(int LeadChargesID = 0)
        {
            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }

            var enumData = from EnumLeadCharges e in Enum.GetValues(typeof(EnumLeadCharges))
                           select new
                           {
                               ID = (int)e,
                               Name = GetEnumName((int)e)
                           };
            ViewBag.EnumList = new SelectList(enumData, "ID", "Name");

            if (LeadChargesID > 0)
            {
                LeadChargeModel leadChargeModel = new LeadChargeModel();
                leadChargeModel.LeadChargesID = LeadChargesID;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiCollections.API_GetLeadChargesID);
                request.Method = "POST";
                request.KeepAlive = true;
                request.ContentType = "application/json; charset=utf-8";
                request.Headers.Add("Authorization", "Bearer " + Session["access_token"]);
                //ChangePasswordViewModel obj = new ChangePasswordViewModel();
                //model.BookingID = Convert.ToInt32(BookingID);
                //model.Status = status;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(leadChargeModel);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string myResponse = "";
                using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    myResponse = sr.ReadToEnd();
                }

                leadChargeModel = (new JavaScriptSerializer()).Deserialize<LeadChargeModel>(myResponse);
                
                return View(leadChargeModel);
            }

           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LeadCharges(LeadChargeModel model)
        {
            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiCollections.API_SaveLeadCharges);
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
                var enumData = from EnumLeadCharges e in Enum.GetValues(typeof(EnumLeadCharges))
                               select new
                               {
                                   ID = (int)e,
                                   Name = GetEnumName((int)e)
                               };
                ViewBag.EnumList = new SelectList(enumData, "ID", "Name");
                return View(model);
            }

            return RedirectToAction("GetAllLeadCharges", "LeadCharges");

        }


    }
}