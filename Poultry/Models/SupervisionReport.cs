﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Poultry.Models
{
    public class SupervisionReport
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public FarmerLog Log { get; set; }
        public List<WeeklyReport> Reports { get; set; }
        public ExtraData ExtraData { get; set; }
        

        

        public int TotalDeath
        {
            get
            {
                var count = 0;
                foreach (var r in Reports)
                {
                    count += r.Day1_TotalDeathCount
                                + r.Day2_TotalDeathCount
                                + r.Day3_TotalDeathCount
                                + r.Day4_TotalDeathCount
                                + r.Day5_TotalDeathCount
                                + r.Day6_TotalDeathCount
                                + r.Day7_TotalDeathCount;
                }
                return count;
            }
        }
    }
}