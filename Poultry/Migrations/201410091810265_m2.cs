namespace Poultry.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.Item_Id)
                .ForeignKey("dbo.VendorLogs", t => t.VendorLog_Id)
                .Index(t => t.Item_Id)
                .Index(t => t.VendorLog_Id);
            
            CreateTable(
                "dbo.FarmerLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ActivityFlag = c.Boolean(nullable: false),
                        Farmer_Id = c.Int(),
                        Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Farmers", t => t.Farmer_Id)
                .ForeignKey("dbo.Items", t => t.Item_Id)
                .Index(t => t.Farmer_Id)
                .Index(t => t.Item_Id);
            
            CreateTable(
                "dbo.Traders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        ContactNo = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
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
                        Amount = c.Int(nullable: false),
                        Vendor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vendors", t => t.Vendor_Id)
                .Index(t => t.Vendor_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.VendorPaymentLogs", new[] { "Vendor_Id" });
            DropIndex("dbo.CostSheets", new[] { "Item_Id" });
            DropIndex("dbo.ConsumptionLogs", new[] { "Item_Id" });
            DropIndex("dbo.ConsumptionLogs", new[] { "For_Id" });
            DropIndex("dbo.FarmerLogs", new[] { "Item_Id" });
            DropIndex("dbo.FarmerLogs", new[] { "Farmer_Id" });
            DropIndex("dbo.ItemTransactions", new[] { "VendorLog_Id" });
            DropIndex("dbo.ItemTransactions", new[] { "Item_Id" });
            DropIndex("dbo.VendorLogs", new[] { "Vendor_Id" });
            DropIndex("dbo.Stocks", new[] { "Item_Id" });
            DropForeignKey("dbo.VendorPaymentLogs", "Vendor_Id", "dbo.Vendors");
            DropForeignKey("dbo.CostSheets", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.ConsumptionLogs", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.ConsumptionLogs", "For_Id", "dbo.Items");
            DropForeignKey("dbo.FarmerLogs", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.FarmerLogs", "Farmer_Id", "dbo.Farmers");
            DropForeignKey("dbo.ItemTransactions", "VendorLog_Id", "dbo.VendorLogs");
            DropForeignKey("dbo.ItemTransactions", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.VendorLogs", "Vendor_Id", "dbo.Vendors");
            DropForeignKey("dbo.Stocks", "Item_Id", "dbo.Items");
            DropTable("dbo.VendorPaymentLogs");
            DropTable("dbo.CostSheets");
            DropTable("dbo.ConsumptionLogs");
            DropTable("dbo.Traders");
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
