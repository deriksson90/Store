namespace Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class instHelp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "InstHelp", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "InstHelp");
        }
    }
}
