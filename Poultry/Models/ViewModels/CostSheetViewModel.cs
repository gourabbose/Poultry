using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poultry.Models.ViewModels
{
    public class CostSheetViewModel
    {
        public Item Items { get; set; }
        public int RatePerKg { get; set; }
        public int Amount1 { get; set; }
        public int Qty1 { get; set; }
        public int Amount2 { get; set; }
        public int Qty2 { get; set; }
        public int Amount3 { get; set; }
        public int Qty3 { get; set; }
        public string Remarks { get; set; }
    }
}