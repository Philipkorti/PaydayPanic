namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "PictureURL", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Highscores", "HighestScore");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Highscores", "HighestScore", c => c.Double(nullable: false));
            AlterColumn("dbo.Items", "PictureURL", c => c.String(nullable: false, maxLength: 25));
        }
    }
}
