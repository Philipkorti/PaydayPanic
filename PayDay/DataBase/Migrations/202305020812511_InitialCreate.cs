namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Highscores", "Ranks_Id", "dbo.Ranks");
            DropIndex("dbo.Highscores", new[] { "Ranks_Id" });
            RenameColumn(table: "dbo.Highscores", name: "Ranks_Id", newName: "RankID");
            AlterColumn("dbo.Highscores", "RankID", c => c.Int(nullable: false));
            CreateIndex("dbo.Highscores", "RankID");
            AddForeignKey("dbo.Highscores", "RankID", "dbo.Ranks", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Highscores", "RankID", "dbo.Ranks");
            DropIndex("dbo.Highscores", new[] { "RankID" });
            AlterColumn("dbo.Highscores", "RankID", c => c.Int());
            RenameColumn(table: "dbo.Highscores", name: "RankID", newName: "Ranks_Id");
            CreateIndex("dbo.Highscores", "Ranks_Id");
            AddForeignKey("dbo.Highscores", "Ranks_Id", "dbo.Ranks", "Id");
        }
    }
}
