namespace Poultry.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class w11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VendorLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Vendor_Id = c.Int(),
                        Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vendors", t => t.Vendor_Id)
                .ForeignKey("dbo.Items", t => t.Item_Id)
                .Index(t => t.Vendor_Id)
                .Index(t => t.Item_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.VendorLogs", new[] { "Item_Id" });
            DropIndex("dbo.VendorLogs", new[] { "Vendor_Id" });
            DropForeignKey("dbo.VendorLogs", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.VendorLogs", "Vendor_Id", "dbo.Vendors");
            DropTable("dbo.VendorLogs");
        }
    }
}
