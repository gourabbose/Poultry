﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poultry.Models.ViewModels
{
    public class Consumption
    {
        public Item Items { get; set; }
        public int Ratio1 { get; set; }
        public int Qty1 { get; set; }
        public int Ratio2 { get; set; }
        public int Qty2 { get; set; }
        public int Ratio3 { get; set; }
        public int Qty3 { get; set; }
        public int TotalQty { get; set; }
    }
}