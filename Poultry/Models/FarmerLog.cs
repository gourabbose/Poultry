using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Poultry.Models
{
    public class FarmerLog
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Farmer Farmer { get; set; }
        public DateTime Date { get; set; }
        public List<ItemTransaction> Items { get; set; }
        public int Payment { get; set; }
        public string PaymentMethod { get; set; }
        public bool ActivityFlag { get; set; }
        public int Lifted { get; set; }
        public int TotalDeath { get; set; }
    }
}