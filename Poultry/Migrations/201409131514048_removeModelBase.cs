namespace Poultry.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeModelBase : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Vendors", "CreatedOn");
            DropColumn("dbo.Vendors", "LastModifiedOn");
            DropColumn("dbo.Vendors", "IsDeleted");
            DropColumn("dbo.Farmers", "CreatedOn");
            DropColumn("dbo.Farmers", "LastModifiedOn");
            DropColumn("dbo.Farmers", "IsDeleted");
            DropColumn("dbo.Items", "CreatedOn");
            DropColumn("dbo.Items", "LastModifiedOn");
            DropColumn("dbo.Items", "IsDeleted");
            DropColumn("dbo.Stocks", "CreatedOn");
            DropColumn("dbo.Stocks", "LastModifiedOn");
            DropColumn("dbo.Stocks", "IsDeleted");
            DropColumn("dbo.FoodItems", "CreatedOn");
            DropColumn("dbo.FoodItems", "LastModifiedOn");
            DropColumn("dbo.FoodItems", "IsDeleted");
            DropColumn("dbo.VendorItems", "CreatedOn");
            DropColumn("dbo.VendorItems", "LastModifiedOn");
            DropColumn("dbo.VendorItems", "IsDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VendorItems", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.VendorItems", "LastModifiedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.VendorItems", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.FoodItems", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItems", "LastModifiedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.FoodItems", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Stocks", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Stocks", "LastModifiedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Stocks", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Items", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Items", "LastModifiedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Items", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Farmers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Farmers", "LastModifiedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Farmers", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Vendors", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Vendors", "LastModifiedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Vendors", "CreatedOn", c => c.DateTime(nullable: false));
        }
    }
}
