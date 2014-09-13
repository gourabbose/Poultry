namespace Poultry.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class w1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stocks", "Item_Id", "dbo.Items");
            DropIndex("dbo.Stocks", new[] { "Item_Id" });
            RenameColumn(table: "dbo.Stocks", name: "Item_Id", newName: "ItemId");
            AddForeignKey("dbo.Stocks", "ItemId", "dbo.Items", "Id", cascadeDelete: true);
            CreateIndex("dbo.Stocks", "ItemId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Stocks", new[] { "ItemId" });
            DropForeignKey("dbo.Stocks", "ItemId", "dbo.Items");
            RenameColumn(table: "dbo.Stocks", name: "ItemId", newName: "Item_Id");
            CreateIndex("dbo.Stocks", "Item_Id");
            AddForeignKey("dbo.Stocks", "Item_Id", "dbo.Items", "Id");
        }
    }
}
