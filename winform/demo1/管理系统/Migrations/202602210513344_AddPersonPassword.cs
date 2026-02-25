namespace ManageSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPersonPassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonT", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PersonT", "Password");
        }
    }
}
