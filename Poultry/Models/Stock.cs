
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Poultry.Models
{
    public class Stock 
    {
        [Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public Item Item { get; set; }
        [DisplayName("Quantity")]
        public int Quantity { get; set; }
        public StockType Type { get; set; } 
    }

    //public enum StockType
    //{
    //    Undefined,
    //    FoodItem,
    //    VendorItem,
    //    Chicken
    //}
}