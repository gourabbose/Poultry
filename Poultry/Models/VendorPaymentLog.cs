using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poultry.Models
{
    public class VendorPaymentLog
    {
        public DateTime Date { get; set; }
        public int Id { get; set; }
        public Vendor Vendor { get; set; }
        public int Amount { get; set; }
    }
}