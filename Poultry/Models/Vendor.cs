﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Poultry.Models
{
    public class Vendor
    {
        [Required(ErrorMessage="Please Select a Vendor.")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DisplayName("Contact No")]
        public string ContactNo { get; set; }
        public bool IsDeleted { get; set; }
        [Display(Name="Amount Due")]
        public int Due { get; set; }
    }
}