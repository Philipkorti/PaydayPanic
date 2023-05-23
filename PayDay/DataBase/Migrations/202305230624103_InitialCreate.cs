namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Casinos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatisticsID = c.Int(nullable: false),
                        CasinoWinCount = c.Int(nullable: false),
                        GameCasinoCount = c.Int(nullable: false),
                        WinSeven = c.Int(nullable: false),
                        WinMoneyBag = c.Int(nullable: false),
                        WinHeart = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Statistics", t => t.StatisticsID, cascadeDelete: true)
                .Index(t => t.StatisticsID);
            
            CreateTable(
                "dbo.Statistics",
                c => new
                    {
                        StatisticID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        GameCount = c.Int(nullable: false),
                        GameMoneyWin = c.Double(nullable: false),
                        GameMoneyLose = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.StatisticID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Highscores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Elo = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        RankID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ranks", t => t.RankID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.RankID);
            
            CreateTable(
                "dbo.Ranks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rank = c.String(),
                        RankURL = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        PictureURL = c.String(nullable: false, maxLength: 50),
                        Title = c.String(nullable: false, maxLength: 50),
                        Price = c.Double(nullable: false),
                        InStock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID);
            
            CreateTable(
                "dbo.Shops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatisticsID = c.Int(nullable: false),
                        BoughtGamesCount = c.Int(nullable: false),
                        GamesSoldCount = c.Int(nullable: false),
                        OutputMoney = c.Double(nullable: false),
                        InputMoney = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Statistics", t => t.StatisticsID, cascadeDelete: true)
                .Index(t => t.StatisticsID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shops", "StatisticsID", "dbo.Statistics");
            DropForeignKey("dbo.Highscores", "UserID", "dbo.Users");
            DropForeignKey("dbo.Highscores", "RankID", "dbo.Ranks");
            DropForeignKey("dbo.Casinos", "StatisticsID", "dbo.Statistics");
            DropForeignKey("dbo.Statistics", "UserID", "dbo.Users");
            DropIndex("dbo.Shops", new[] { "StatisticsID" });
            DropIndex("dbo.Highscores", new[] { "RankID" });
            DropIndex("dbo.Highscores", new[] { "UserID" });
            DropIndex("dbo.Statistics", new[] { "UserID" });
            DropIndex("dbo.Casinos", new[] { "StatisticsID" });
            DropTable("dbo.Shops");
            DropTable("dbo.Items");
            DropTable("dbo.Ranks");
            DropTable("dbo.Highscores");
            DropTable("dbo.Users");
            DropTable("dbo.Statistics");
            DropTable("dbo.Casinos");
        }
    }
}
