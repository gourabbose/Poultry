namespace Poultry.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VendorLogs", "Payment", c => c.Int(nullable: false));
            AddColumn("dbo.VendorPaymentLogs", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VendorPaymentLogs", "Date");
            DropColumn("dbo.VendorLogs", "Payment");
        }
    }
}
