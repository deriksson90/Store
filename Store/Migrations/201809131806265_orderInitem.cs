namespace Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderInitem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Order_OrderId", c => c.Int());
            CreateIndex("dbo.Items", "Order_OrderId");
            AddForeignKey("dbo.Items", "Order_OrderId", "dbo.Orders", "OrderId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.Items", new[] { "Order_OrderId" });
            DropColumn("dbo.Items", "Order_OrderId");
        }
    }
}
