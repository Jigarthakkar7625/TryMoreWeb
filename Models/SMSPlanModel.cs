using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace TryMoreWeb.Models
{

    public class SMSPlanModel : BaseModel
    {
        public int SMSPlanID { get; set; }
        public string PlanName { get; set; }
        public long NoOfSMS { get; set; }
        public decimal Amount { get; set; }
        public int IsActive { get; set; }
        public Boolean IsActiveBool { get; set; }
    }
}