using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.ComponentModel;

namespace TryMoreWeb.Models
{
  
    public class BrandModel : BaseModel
    {
        public long BrandID { get; set; }
        public string BrandName { get; set; }
        public string BrandDescription { get; set; }

    }

   
}