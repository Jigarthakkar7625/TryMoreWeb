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
    public class ProductController : Controller
    {
        public ActionResult GetProducts()
        {

            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiCollections.API_GetProducts);
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
            List<ProductModel> productModels = (new JavaScriptSerializer()).Deserialize<List<ProductModel>>(myResponse);

            return View(productModels);
        }


        public ActionResult Product(int ProductID = 0)
        {
            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }

            ProductModel productModel = new ProductModel();
            productModel.ProductID = ProductID;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiCollections.API_GetProductById);
            request.Method = "POST";
            request.KeepAlive = true;
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("Authorization", "Bearer " + Session["access_token"]);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(productModel);
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string myResponse = "";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
            {
                myResponse = sr.ReadToEnd();
            }

            productModel = (new JavaScriptSerializer()).Deserialize<ProductModel>(myResponse);
            if (productModel.Picture != "" && productModel.Picture != null)
            {
                productModel.Picture = "/Upload/Product" + productModel.Picture;
            }

            ViewBag.Categories = new SelectList(productModel.lstCategories, "CategoryID", "CategoryName");
            ViewBag.Brands = new SelectList(productModel.lstBrands, "BrandID", "BrandName");

            return View(productModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Product(ProductModel model)
        {
            if (Session["access_token"] == null)
            {
                return RedirectToAction("Login", "Register");
            }

            if (model.files != null)
            {
                model.FileExt = Path.GetExtension(model.files.FileName);
            }
            else
            {
                model.FileExt = "";
            }
            HttpPostedFileBase httpPostedFileBase = model.files;
            model.files = null;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiCollections.API_SaveProduct);
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
            ProductModel m = (new JavaScriptSerializer()).Deserialize<ProductModel>(myResponse);

            TempData["success"] = m.success;
            TempData["message"] = m.message;
            if (m.success == false)
            {
                return View(model);
            }

            if (httpPostedFileBase != null && m.Picture != null && m.Picture != "")
            {
                string currentDirectory = Path.GetDirectoryName(m.Picture);

                string fullPathOnly = System.IO.Path.Combine(
                                       Server.MapPath("~/Upload/Product/" + currentDirectory));

                if (!Directory.Exists(fullPathOnly))
                {
                    Directory.CreateDirectory(fullPathOnly);
                }

                string filePath = System.IO.Path.Combine(
                                       Server.MapPath("~/Upload/Product/" + m.Picture));
                httpPostedFileBase.SaveAs(filePath);

            }

            return RedirectToAction("GetProducts", "Product");

        }


    }
}