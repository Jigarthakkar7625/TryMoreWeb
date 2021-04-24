using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TryMoreWeb
{
    public static class ApiCollections
    {
        //base url
        private readonly static string baseAPIUrl = System.Configuration.ConfigurationManager.AppSettings["API_URL"];

        public static readonly string API_USER_REGISTER = baseAPIUrl + "Register/Register";
        public static readonly string API_USER_FORGOTPASSWORD = baseAPIUrl + "Register/ForgotPassword";
        public static readonly string API_USER_LOGIN = baseAPIUrl + "Register/Login";
        public static readonly string API_GET_ALL_USERS = baseAPIUrl + "User/GetUsers";
        public static readonly string API_GET_SELLER_APPROVE = baseAPIUrl + "User/SellerApprove";
        public static readonly string API_CHANGE_PASSWORD = baseAPIUrl + "User/ChangePassword";
        public static readonly string API_USER_PROFILE = baseAPIUrl + "User/GetUsersProfile";
        public static readonly string API_USER_UPDTAEPROFILE = baseAPIUrl + "User/UsersUpdateProfile";
        public static readonly string API_USER_UPLOAD = baseAPIUrl + "User/Upload";

        public static readonly string API_SMSPlan = baseAPIUrl + "SMSPlan/GetSMSPlan";
        public static readonly string API_SaveSMSPlan = baseAPIUrl + "SMSPlan/SaveSMSPlan";
        public static readonly string API_GetSMSPlanByID = baseAPIUrl + "SMSPlan/GetSMSPlanById";

        public static readonly string API_GetLeadCharges = baseAPIUrl + "LeadCharges/GetLeadCharges";
        public static readonly string API_SaveLeadCharges = baseAPIUrl + "LeadCharges/SaveLeadCharges";
        public static readonly string API_GetLeadChargesID = baseAPIUrl + "LeadCharges/GetLeadChargesId";

        public static readonly string API_GetCategories = baseAPIUrl + "Category/GetCategories";
        public static readonly string API_SaveCategory = baseAPIUrl + "Category/SaveCategory";
        public static readonly string API_GetCategoryId = baseAPIUrl + "Category/GetCategoryId";
        public static readonly string API_Category_Upload = baseAPIUrl + "Category/Upload";

        public static readonly string API_GetBrands = baseAPIUrl + "Brand/GetBrands";
        public static readonly string API_SaveBrand = baseAPIUrl + "Brand/SaveBrand";
        public static readonly string API_GetBrandId = baseAPIUrl + "Brand/GetBrandId";
        public static readonly string API_Brand_Upload = baseAPIUrl + "Brand/Upload";

        public static readonly string API_GetProducts = baseAPIUrl + "Product/GetProducts";
        public static readonly string API_SaveProduct = baseAPIUrl + "Product/SaveProduct";
        public static readonly string API_GetProductById = baseAPIUrl + "Product/GetProductById";
        public static readonly string API_ProductApprove = baseAPIUrl + "Product/ProductApprove";
        public static readonly string API_Product_Upload = baseAPIUrl + "Product/Upload";
    }
}