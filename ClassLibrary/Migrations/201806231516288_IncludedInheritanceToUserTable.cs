namespace ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncludedInheritanceToUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTables", "NewMessages", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTables", "NewMessages");
        }
    }
}
