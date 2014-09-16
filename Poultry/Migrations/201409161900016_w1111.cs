namespace Poultry.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class w1111 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FarmerLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Farmer_Id = c.Int(),
                        Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Farmers", t => t.Farmer_Id)
                .ForeignKey("dbo.Items", t => t.Item_Id)
                .Index(t => t.Farmer_Id)
                .Index(t => t.Item_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.FarmerLogs", new[] { "Item_Id" });
            DropIndex("dbo.FarmerLogs", new[] { "Farmer_Id" });
            DropForeignKey("dbo.FarmerLogs", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.FarmerLogs", "Farmer_Id", "dbo.Farmers");
            DropTable("dbo.FarmerLogs");
        }
    }
}
