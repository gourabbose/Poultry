using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Poultry.Models
{
    public class CostSheet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Item Item { get; set; } 
        [Display(Name="Rate per Kg")]
        public int RatePerKg { get; set; }
        [Display(Name = "Quantity")]
        public int BPS_Qty { get; set; }
        [Display(Name = "Amount")]
        public int BPS_Amt { get; set; }
        [Display(Name = "Quantity")]
        public int BS_Qty { get; set; }
        [Display(Name = "Amount")]
        public int BS_Amt { get; set; }
        [Display(Name = "Quantity")]
        public int BF_Qty { get; set; }
        [Display(Name = "Amount")]
        public int BF_Amt { get; set; }
        public string Remarks { get; set; }
    }
}