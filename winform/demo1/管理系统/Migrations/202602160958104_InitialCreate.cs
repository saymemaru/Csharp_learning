namespace ManageSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuT",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuText = c.String(),
                        MenuImage = c.String(),
                        MenuPage = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MenuT");
        }
    }
}
