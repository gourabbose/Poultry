﻿using Poultry.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Poultry.DbContexts
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("DbConnection") { }

        #region DbSets
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<Farmer> Farmer { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Stock> Stock { get; set; }
        //public DbSet<FoodItems> FoodItems { get; set; }
        //public DbSet<VendorItems> VendorItems { get; set; }
        public DbSet<VendorLog> VendorLog { get; set; }
        public DbSet<FarmerLog> FarmerLog { get; set; }
        public DbSet<TraderLog> TraderLog { get; set; }
        #endregion

        public DbSet<Trader> Traders { get; set; }
        public DbSet<ConsumptionLog> ConsumptionLogs { get; set; }
        public DbSet<CostSheet> CostSheets { get; set; }
        public DbSet<VendorPaymentLog> VendorPayments { get; set; }
        public DbSet<ProductionLog> ProductionLogs { get; set; }
        public DbSet<WeeklyReport> WeeklyReports { get; set; }
        public DbSet<SupervisionReport> Reports { get; set; }
        public DbSet<Lifting> Lifting { get; set; }
        public DbSet<ExtraData> ExtraData { get; set; }
    }
}