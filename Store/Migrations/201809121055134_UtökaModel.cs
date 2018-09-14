namespace Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UtökaModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "Weight", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OrderDetails", "Volume", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Items", "ItemArtUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "ItemArtUrl", c => c.String(maxLength: 1024));
            DropColumn("dbo.OrderDetails", "Volume");
            DropColumn("dbo.OrderDetails", "Weight");
        }
    }
}
