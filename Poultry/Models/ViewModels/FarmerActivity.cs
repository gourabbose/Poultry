using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poultry.Models.ViewModels
{
    public class FarmerActivity
    {
        public Farmer Farmer { get; set; }
        public DateTime ActivityDate { get; set; }
        public int NoOfChicks { get; set; }
    }
}