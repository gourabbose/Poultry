using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Poultry.Models
{
    public class ConsumptionLog
    {
        [Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Item For { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
    }
}