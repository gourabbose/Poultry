using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Poultry.Models
{
    public class Lifting
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public IEnumerable<FarmerLog> FarmerLogs { get; set; }
        public TraderLog TraderLog { get; set; }
        public DateTime Date { get; set; }
        public string DCNo { get; set; }
        public string TotalWt { get; set; }
        public string AvgWt { get; set; }


    }
}