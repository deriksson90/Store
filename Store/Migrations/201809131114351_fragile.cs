namespace Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fragile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Fragile", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Fragile");
        }
    }
}
