using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.ComponentModel;

namespace TryMoreWeb.Models
{
    public enum EnumLeadCharges
    {
        [Description("< 200")]
        LeadCharges1 = 1,
        [Description("201 To 500")]
        LeadCharges2 = 2,
        [Description("501 To 2000")]
        LeadCharges3 = 3,
        [Description("2001 To 5000")]
        LeadCharges4 = 4,
        [Description("5001 To 10000")]
        LeadCharges5 = 5,
        [Description("10001 To 20000")]
        LeadCharges6 = 6,
        [Description("> 20000")]
        LeadCharges7 = 7
    }
    public class LeadChargeModel : BaseModel
    {
        public int LeadChargesID { get; set; }
        public int LeadProductAmountID { get; set; }
        public string LeadProductAmountName { get; set; }
        public decimal LeadCharges { get; set; }

    }

   
}