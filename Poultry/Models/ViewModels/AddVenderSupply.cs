using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poultry.Models.ViewModels
{
    public class AddVendorSupply
    {
        public IEnumerable<VendorItems> Items { get; set; }
        public IEnumerable<Stock> Supply { get; set; }
    }
}