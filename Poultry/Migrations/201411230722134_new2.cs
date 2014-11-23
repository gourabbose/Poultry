namespace Poultry.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VendorLogs", "FolioNo", c => c.String());
            AddColumn("dbo.Liftings", "DCNo", c => c.String());
            AddColumn("dbo.Liftings", "TotalWt", c => c.String());
            AddColumn("dbo.Liftings", "AvgWt", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Liftings", "AvgWt");
            DropColumn("dbo.Liftings", "TotalWt");
            DropColumn("dbo.Liftings", "DCNo");
            DropColumn("dbo.VendorLogs", "FolioNo");
        }
    }
}
