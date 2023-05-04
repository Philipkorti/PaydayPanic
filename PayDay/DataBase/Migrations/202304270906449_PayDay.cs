namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PayDay : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Highscores", "Rank", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Highscores", "Rank");
        }
    }
}
