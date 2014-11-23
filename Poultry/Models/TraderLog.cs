using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Poultry.Models
{
    public class TraderLog
    {

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Trader Trader { get; set; }
        public int ChickenCount { get; set; }
        public int Price { get; set; }
        public int Payment { get; set; }
        public string PaymentMethod { get; set; }
    }
}