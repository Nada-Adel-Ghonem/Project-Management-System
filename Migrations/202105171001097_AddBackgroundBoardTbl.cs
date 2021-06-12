namespace Final_Project_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBackgroundBoardTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Boards", "Background", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Boards", "Background");
        }
    }
}
