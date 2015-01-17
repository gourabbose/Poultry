using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Poultry.Models
{
    public class WeeklyReport
    {
        public WeeklyReport() { }
        public WeeklyReport(int weekNo)
        {
            WeekNo = weekNo;
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int WeekNo { get; set; }

        public int AvgWt { get; set; }
        public string Remarks { get; set; }
        public string FCR { get; set; }


        public int Day1_ChickCount { get; set; }
        public int Day1_DeathCountPerDay { get; set; }
        public string Day1_DeathRemarks { get; set; }
        public int Day1_TotalDeathCount { get; set; }
        public float Day1_FoodConsumption { get; set; }
        public float Day1_AvgConsumption { get; set; }
        public string Day1_Remarks { get; set; }
        public string Day1_InStock { get; set; }
        public float Day1_CurrentWt { get; set; }
        public string Day1_FCR { get; set; }

        public int Day2_ChickCount { get; set; }
        public int Day2_DeathCountPerDay { get; set; }
        public string Day2_DeathRemarks { get; set; }
        public int Day2_TotalDeathCount { get; set; }
        public float Day2_FoodConsumption { get; set; }
        public float Day2_AvgConsumption { get; set; }
        public string Day2_Remarks { get; set; }
        public string Day2_InStock { get; set; }
        public float Day2_CurrentWt { get; set; }
        public string Day2_FCR { get; set; }

        public int Day3_ChickCount { get; set; }
        public int Day3_DeathCountPerDay { get; set; }
        public string Day3_DeathRemarks { get; set; }
        public int Day3_TotalDeathCount { get; set; }
        public float Day3_FoodConsumption { get; set; }
        public float Day3_AvgConsumption { get; set; }
        public string Day3_Remarks { get; set; }
        public string Day3_InStock { get; set; }
        public float Day3_CurrentWt { get; set; }
        public string Day3_FCR { get; set; }

        public int Day4_ChickCount { get; set; }
        public int Day4_DeathCountPerDay { get; set; }
        public string Day4_DeathRemarks { get; set; }
        public int Day4_TotalDeathCount { get; set; }
        public float Day4_FoodConsumption { get; set; }
        public float Day4_AvgConsumption { get; set; }
        public string Day4_Remarks { get; set; }
        public string Day4_InStock { get; set; }
        public float Day4_CurrentWt { get; set; }
        public string Day4_FCR { get; set; }

        public int Day5_ChickCount { get; set; }
        public int Day5_DeathCountPerDay { get; set; }
        public string Day5_DeathRemarks { get; set; }
        public int Day5_TotalDeathCount { get; set; }
        public float Day5_FoodConsumption { get; set; }
        public float Day5_AvgConsumption { get; set; }
        public string Day5_Remarks { get; set; }
        public string Day5_InStock { get; set; }
        public float Day5_CurrentWt { get; set; }
        public string Day5_FCR { get; set; }

        public int Day6_ChickCount { get; set; }
        public int Day6_DeathCountPerDay { get; set; }
        public string Day6_DeathRemarks { get; set; }
        public int Day6_TotalDeathCount { get; set; }
        public float Day6_FoodConsumption { get; set; }
        public float Day6_AvgConsumption { get; set; }
        public string Day6_Remarks { get; set; }
        public string Day6_InStock { get; set; }
        public float Day6_CurrentWt { get; set; }
        public string Day6_FCR { get; set; }

        public int Day7_ChickCount { get; set; }
        public int Day7_DeathCountPerDay { get; set; }
        public string Day7_DeathRemarks { get; set; }
        public int Day7_TotalDeathCount { get; set; }
        public float Day7_FoodConsumption { get; set; }
        public float Day7_AvgConsumption { get; set; }
        public string Day7_Remarks { get; set; }
        public string Day7_InStock { get; set; }
        public float Day7_CurrentWt { get; set; }
        public string Day7_FCR { get; set; }

        [NotMapped]
        public string ExpectedWt
        {
            get
            {
                string val = "";
                switch (WeekNo)
                {
                    case 1:
                        val = "175 - 185";
                        break;
                    case 2:
                        val = "420 - 450";
                        break;
                    case 3:
                        val = "800";
                        break;
                    case 4:
                        val = "1320 - 1400";
                        break;
                    case 5:
                        val = "1850 - 1950";
                        break;
                    case 6:
                        val = "2350 - 2500";
                        break;
                    case 7:
                        val = " ";
                        break;
                }

                return val;
            }
        }
        [NotMapped]
        public string ExpFCR
        {
            get
            {
                string val = "";
                switch (WeekNo)
                {
                    case 1:
                        val = "0.85 - 0.87";
                        break;
                    case 2:
                        val = "1.20";
                        break;
                    case 3:
                        val = "1.30 - 1.32";
                        break;
                    case 4:
                        val = "1.43";
                        break;
                    case 5:
                        val = "1.55 - 1.57";
                        break;
                    case 6:
                        val = "1.70 - 1.75";
                        break;
                    case 7:
                        val = " ";
                        break;
                }

                return val;
            }
        }
        [NotMapped]
        public string ExpFoodIntake
        {
            get
            {
                string val = "";
                switch (WeekNo)
                {
                    case 1:
                        val = "20 - 22";
                        break;
                    case 2:
                        val = "42 - 46";
                        break;
                    case 3:
                        val = "75 - 80";
                        break;
                    case 4:
                        val = "110 - 120";
                        break;
                    case 5:
                        val = "130 - 140";
                        break;
                    case 6:
                        val = "125 - 135";
                        break;
                    case 7:
                        val = " ";
                        break;
                }

                return val;
            }
        }

    }
}