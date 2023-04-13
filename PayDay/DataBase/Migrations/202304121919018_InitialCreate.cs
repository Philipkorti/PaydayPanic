namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Golds",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                        InStock = c.Int(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ItemID)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        Elo = c.String(),
                        highscore = c.Int(nullable: false),
                        Goldscore = c.Int(nullable: false),
                        GameCount = c.Int(nullable: false),
                        GameTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "User_UserId", "dbo.Users");
            DropIndex("dbo.Items", new[] { "User_UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Items");
            DropTable("dbo.Golds");
        }
    }
}
