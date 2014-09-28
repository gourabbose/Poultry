
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Poultry.Models
{
    public class Item
    {
        [Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required][DisplayName("Item Name")]
        public string Name { get; set; }
        public StockType Type { get; set; }
        [Required]
        public string Unit { get; set; }
        public bool IsDeleted { get; set; }
    }
    public enum StockType
    {
        Undefined,
        FoodItem,
        VendorItem,
        Chicken
    }
}