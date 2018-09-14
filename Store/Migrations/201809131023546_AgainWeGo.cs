namespace Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgainWeGo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Weight", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Items", "Volume", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OrderDetails", "TotalWeight", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OrderDetails", "TotalVolume", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.OrderDetails", "Weight");
            DropColumn("dbo.OrderDetails", "Volume");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "Volume", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OrderDetails", "Weight", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.OrderDetails", "TotalVolume");
            DropColumn("dbo.OrderDetails", "TotalWeight");
            DropColumn("dbo.Items", "Volume");
            DropColumn("dbo.Items", "Weight");
        }
    }
}
