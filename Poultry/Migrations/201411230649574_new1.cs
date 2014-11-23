namespace Poultry.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        ContactNo = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Due = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Farmers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        ContactNo = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                        Unit = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.Item_Id)
                .Index(t => t.Item_Id);
            
            CreateTable(
                "dbo.VendorLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Payment = c.Int(nullable: false),
                        Vendor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vendors", t => t.Vendor_Id)
                .Index(t => t.Vendor_Id);
            
            CreateTable(
                "dbo.ItemTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Qty = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Item_Id = c.Int(),
                        VendorLog_Id = c.Int(),
                        FarmerLog_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.Item_Id)
                .ForeignKey("dbo.VendorLogs", t => t.VendorLog_Id)
                .ForeignKey("dbo.FarmerLogs", t => t.FarmerLog_Id)
                .Index(t => t.Item_Id)
                .Index(t => t.VendorLog_Id)
                .Index(t => t.FarmerLog_Id);
            
            CreateTable(
                "dbo.FarmerLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Payment = c.Int(nullable: false),
                        PaymentMethod = c.String(),
                        ActivityFlag = c.Boolean(nullable: false),
                        Lifted = c.Int(nullable: false),
                        TotalDeath = c.Int(nullable: false),
                        Farmer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Farmers", t => t.Farmer_Id)
                .Index(t => t.Farmer_Id);
            
            CreateTable(
                "dbo.TraderLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChickenCount = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Payment = c.Int(nullable: false),
                        PaymentMethod = c.String(),
                        Trader_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Traders", t => t.Trader_Id)
                .Index(t => t.Trader_Id);
            
            CreateTable(
                "dbo.Traders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        ContactNo = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        PaymentDue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConsumptionLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Ratio = c.Int(nullable: false),
                        For_Id = c.Int(),
                        Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.For_Id)
                .ForeignKey("dbo.Items", t => t.Item_Id)
                .Index(t => t.For_Id)
                .Index(t => t.Item_Id);
            
            CreateTable(
                "dbo.CostSheets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        RatePerKg = c.Int(nullable: false),
                        BPS_Qty = c.Int(nullable: false),
                        BPS_Amt = c.Int(nullable: false),
                        BS_Qty = c.Int(nullable: false),
                        BS_Amt = c.Int(nullable: false),
                        BF_Qty = c.Int(nullable: false),
                        BF_Amt = c.Int(nullable: false),
                        Remarks = c.String(),
                        Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.Item_Id)
                .Index(t => t.Item_Id);
            
            CreateTable(
                "dbo.VendorPaymentLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Int(nullable: false),
                        Vendor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vendors", t => t.Vendor_Id)
                .Index(t => t.Vendor_Id);
            
            CreateTable(
                "dbo.ProductionLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Qty = c.Int(nullable: false),
                        Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.Item_Id)
                .Index(t => t.Item_Id);
            
            CreateTable(
                "dbo.WeeklyReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WeekNo = c.Int(nullable: false),
                        AvgWt = c.Int(nullable: false),
                        Remarks = c.String(),
                        FCR = c.String(),
                        Day1_ChickCount = c.Int(nullable: false),
                        Day1_DeathCountPerDay = c.Int(nullable: false),
                        Day1_DeathRemarks = c.String(),
                        Day1_TotalDeathCount = c.Int(nullable: false),
                        Day1_FoodConsumption = c.Single(nullable: false),
                        Day1_AvgConsumption = c.Single(nullable: false),
                        Day1_Remarks = c.String(),
                        Day1_InStock = c.String(),
                        Day2_ChickCount = c.Int(nullable: false),
                        Day2_DeathCountPerDay = c.Int(nullable: false),
                        Day2_DeathRemarks = c.String(),
                        Day2_TotalDeathCount = c.Int(nullable: false),
                        Day2_FoodConsumption = c.Single(nullable: false),
                        Day2_AvgConsumption = c.Single(nullable: false),
                        Day2_Remarks = c.String(),
                        Day2_InStock = c.String(),
                        Day3_ChickCount = c.Int(nullable: false),
                        Day3_DeathCountPerDay = c.Int(nullable: false),
                        Day3_DeathRemarks = c.String(),
                        Day3_TotalDeathCount = c.Int(nullable: false),
                        Day3_FoodConsumption = c.Single(nullable: false),
                        Day3_AvgConsumption = c.Single(nullable: false),
                        Day3_Remarks = c.String(),
                        Day3_InStock = c.String(),
                        Day4_ChickCount = c.Int(nullable: false),
                        Day4_DeathCountPerDay = c.Int(nullable: false),
                        Day4_DeathRemarks = c.String(),
                        Day4_TotalDeathCount = c.Int(nullable: false),
                        Day4_FoodConsumption = c.Single(nullable: false),
                        Day4_AvgConsumption = c.Single(nullable: false),
                        Day4_Remarks = c.String(),
                        Day4_InStock = c.String(),
                        Day5_ChickCount = c.Int(nullable: false),
                        Day5_DeathCountPerDay = c.Int(nullable: false),
                        Day5_DeathRemarks = c.String(),
                        Day5_TotalDeathCount = c.Int(nullable: false),
                        Day5_FoodConsumption = c.Single(nullable: false),
                        Day5_AvgConsumption = c.Single(nullable: false),
                        Day5_Remarks = c.String(),
                        Day5_InStock = c.String(),
                        Day6_ChickCount = c.Int(nullable: false),
                        Day6_DeathCountPerDay = c.Int(nullable: false),
                        Day6_DeathRemarks = c.String(),
                        Day6_TotalDeathCount = c.Int(nullable: false),
                        Day6_FoodConsumption = c.Single(nullable: false),
                        Day6_AvgConsumption = c.Single(nullable: false),
                        Day6_Remarks = c.String(),
                        Day6_InStock = c.String(),
                        Day7_ChickCount = c.Int(nullable: false),
                        Day7_DeathCountPerDay = c.Int(nullable: false),
                        Day7_DeathRemarks = c.String(),
                        Day7_TotalDeathCount = c.Int(nullable: false),
                        Day7_FoodConsumption = c.Single(nullable: false),
                        Day7_AvgConsumption = c.Single(nullable: false),
                        Day7_Remarks = c.String(),
                        Day7_InStock = c.String(),
                        SupervisionReport_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SupervisionReports", t => t.SupervisionReport_Id)
                .Index(t => t.SupervisionReport_Id);
            
            CreateTable(
                "dbo.SupervisionReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Log_Id = c.Int(),
                        ExtraData_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FarmerLogs", t => t.Log_Id)
                .ForeignKey("dbo.ExtraDatas", t => t.ExtraData_Id)
                .Index(t => t.Log_Id)
                .Index(t => t.ExtraData_Id);
            
            CreateTable(
                "dbo.ExtraDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data1 = c.String(),
                        Data2 = c.String(),
                        Data3 = c.String(),
                        Data4 = c.String(),
                        Data5 = c.String(),
                        Data6 = c.String(),
                        Data7 = c.String(),
                        Data8 = c.String(),
                        Data9 = c.String(),
                        Data10 = c.String(),
                        Data11 = c.String(),
                        Data12 = c.String(),
                        Data13 = c.String(),
                        Data14 = c.String(),
                        Data15 = c.String(),
                        Data16 = c.String(),
                        Data17 = c.String(),
                        Data18 = c.String(),
                        Data19 = c.String(),
                        Data20 = c.String(),
                        Data21 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Liftings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        TraderLog_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TraderLogs", t => t.TraderLog_Id)
                .Index(t => t.TraderLog_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Liftings", new[] { "TraderLog_Id" });
            DropIndex("dbo.SupervisionReports", new[] { "ExtraData_Id" });
            DropIndex("dbo.SupervisionReports", new[] { "Log_Id" });
            DropIndex("dbo.WeeklyReports", new[] { "SupervisionReport_Id" });
            DropIndex("dbo.ProductionLogs", new[] { "Item_Id" });
            DropIndex("dbo.VendorPaymentLogs", new[] { "Vendor_Id" });
            DropIndex("dbo.CostSheets", new[] { "Item_Id" });
            DropIndex("dbo.ConsumptionLogs", new[] { "Item_Id" });
            DropIndex("dbo.ConsumptionLogs", new[] { "For_Id" });
            DropIndex("dbo.TraderLogs", new[] { "Trader_Id" });
            DropIndex("dbo.FarmerLogs", new[] { "Farmer_Id" });
            DropIndex("dbo.ItemTransactions", new[] { "FarmerLog_Id" });
            DropIndex("dbo.ItemTransactions", new[] { "VendorLog_Id" });
            DropIndex("dbo.ItemTransactions", new[] { "Item_Id" });
            DropIndex("dbo.VendorLogs", new[] { "Vendor_Id" });
            DropIndex("dbo.Stocks", new[] { "Item_Id" });
            DropForeignKey("dbo.Liftings", "TraderLog_Id", "dbo.TraderLogs");
            DropForeignKey("dbo.SupervisionReports", "ExtraData_Id", "dbo.ExtraDatas");
            DropForeignKey("dbo.SupervisionReports", "Log_Id", "dbo.FarmerLogs");
            DropForeignKey("dbo.WeeklyReports", "SupervisionReport_Id", "dbo.SupervisionReports");
            DropForeignKey("dbo.ProductionLogs", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.VendorPaymentLogs", "Vendor_Id", "dbo.Vendors");
            DropForeignKey("dbo.CostSheets", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.ConsumptionLogs", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.ConsumptionLogs", "For_Id", "dbo.Items");
            DropForeignKey("dbo.TraderLogs", "Trader_Id", "dbo.Traders");
            DropForeignKey("dbo.FarmerLogs", "Farmer_Id", "dbo.Farmers");
            DropForeignKey("dbo.ItemTransactions", "FarmerLog_Id", "dbo.FarmerLogs");
            DropForeignKey("dbo.ItemTransactions", "VendorLog_Id", "dbo.VendorLogs");
            DropForeignKey("dbo.ItemTransactions", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.VendorLogs", "Vendor_Id", "dbo.Vendors");
            DropForeignKey("dbo.Stocks", "Item_Id", "dbo.Items");
            DropTable("dbo.Liftings");
            DropTable("dbo.ExtraDatas");
            DropTable("dbo.SupervisionReports");
            DropTable("dbo.WeeklyReports");
            DropTable("dbo.ProductionLogs");
            DropTable("dbo.VendorPaymentLogs");
            DropTable("dbo.CostSheets");
            DropTable("dbo.ConsumptionLogs");
            DropTable("dbo.Traders");
            DropTable("dbo.TraderLogs");
            DropTable("dbo.FarmerLogs");
            DropTable("dbo.ItemTransactions");
            DropTable("dbo.VendorLogs");
            DropTable("dbo.Stocks");
            DropTable("dbo.Items");
            DropTable("dbo.Farmers");
            DropTable("dbo.Vendors");
            DropTable("dbo.UserProfile");
        }
    }
}
