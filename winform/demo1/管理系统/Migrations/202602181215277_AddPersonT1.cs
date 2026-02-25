namespace ManageSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPersonT1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.PersonT");
            AlterColumn("dbo.PersonT", "PersonId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.PersonT", "PersonId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PersonT");
            AlterColumn("dbo.PersonT", "PersonId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.PersonT", "PersonId");
        }
    }
}
