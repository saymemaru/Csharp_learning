namespace ManageSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoleId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonT", "RoleId", c => c.Int(nullable: false, defaultValue: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PersonT", "RoleId");
        }
    }
}
