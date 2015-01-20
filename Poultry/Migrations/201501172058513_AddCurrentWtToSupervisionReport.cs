namespace Poultry.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCurrentWtToSupervisionReport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SupervisionReports","CurrentWeight",c=>c.Int());
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
