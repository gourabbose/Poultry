using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poultry.Models.ViewModels
{
    public class VendorHistory
    {
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public String Type { get; set; }
        public int LogId { get; set; }
    }
}