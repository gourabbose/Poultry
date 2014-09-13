using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Poultry.Models
{
    public class VendorLog
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Vendor Vendor { get; set; }
        public DateTime Date { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
    }
}