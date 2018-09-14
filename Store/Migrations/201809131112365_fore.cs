namespace Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fore : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.FragileGoods");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FragileGoods",
                c => new
                    {
                        FragileId = c.Int(nullable: false, identity: true),
                        ExtraCost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FragileId);
            
        }
    }
}
