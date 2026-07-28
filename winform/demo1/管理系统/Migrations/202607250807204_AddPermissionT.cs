namespace ManageSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPermissionT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PermissionT",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        MenuPage = c.String(),
                        Functions = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PermissionT");
        }
    }
}
