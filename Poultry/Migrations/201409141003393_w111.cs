namespace Poultry.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class w111 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Farmers", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Farmers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Items", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "IsDeleted");
            DropColumn("dbo.Farmers", "IsDeleted");
            DropColumn("dbo.Farmers", "IsActive");
        }
    }
}
