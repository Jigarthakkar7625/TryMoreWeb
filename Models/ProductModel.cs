using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.ComponentModel;

namespace TryMoreWeb.Models
{
  
    public class ProductModel : BaseModel
    {
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public Nullable<int> SellerID { get; set; }
        public string SellerName { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string CategoryName { get; set; }
        public Nullable<int> BrandID { get; set; }
        public string BrandName { get; set; }
        public Nullable<int> QuantityPerUnit { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> UnitWeight { get; set; }
        public Nullable<int> UnitsInStack { get; set; }
        public string Picture { get; set; }
        public string Ranking { get; set; }
        public string Note { get; set; }
        public Nullable<int> IsApprove { get; set; }
        public Boolean IsApproveBool { get; set; }
        public HttpPostedFileBase files { get; set; }
        public string FileExt { get; set; }
        public List<BrandModel> lstBrands { get; set; }
        public List<CategoryModel> lstCategories { get; set; }
    }

   
}