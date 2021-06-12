namespace Final_Project_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBoardListEventTbls : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Boards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NumOfLists = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        ListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lists", t => t.ListId, cascadeDelete: true)
                .Index(t => t.ListId);
            
            CreateTable(
                "dbo.Lists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NumOfEvents = c.Int(nullable: false),
                        BoardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boards", t => t.BoardId, cascadeDelete: true)
                .Index(t => t.BoardId);
            
            AddColumn("dbo.Users", "NumOfBoards", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "ListId", "dbo.Lists");
            DropForeignKey("dbo.Lists", "BoardId", "dbo.Boards");
            DropForeignKey("dbo.Boards", "UserId", "dbo.Users");
            DropIndex("dbo.Lists", new[] { "BoardId" });
            DropIndex("dbo.Events", new[] { "ListId" });
            DropIndex("dbo.Boards", new[] { "UserId" });
            DropColumn("dbo.Users", "NumOfBoards");
            DropTable("dbo.Lists");
            DropTable("dbo.Events");
            DropTable("dbo.Boards");
        }
    }
}
