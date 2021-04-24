using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.ComponentModel;

namespace TryMoreWeb.Models
{
  
    public class CategoryModel : BaseModel
    {
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

    }

   
}