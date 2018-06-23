namespace ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimePublished = c.DateTime(nullable: false),
                        Body = c.String(),
                        Author_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTables", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.UserTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        email = c.String(),
                        firstName = c.String(),
                        lastName = c.String(),
                        birthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostTables", "Author_Id", "dbo.UserTables");
            DropIndex("dbo.PostTables", new[] { "Author_Id" });
            DropTable("dbo.UserTables");
            DropTable("dbo.PostTables");
        }
    }
}
