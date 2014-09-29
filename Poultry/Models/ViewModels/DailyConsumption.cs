using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poultry.Models.ViewModels
{
    public class Consumption
    {
        public Item Items { get; set; }
        public int Qty { get; set; }
        public Item FoodItems { get; set; }
    }
}