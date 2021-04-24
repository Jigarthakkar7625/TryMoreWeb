using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace TryMoreWeb.Models
{
    public class BaseModel
    {
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }

        public bool success { get; set; } = false;
        public string message { get; set; }
        public int code { get; set; }
        public Object data { get; set; }
    }

}