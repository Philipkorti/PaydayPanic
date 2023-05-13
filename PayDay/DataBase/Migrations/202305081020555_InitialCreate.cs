namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Items", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Description", c => c.String(nullable: false));
        }
    }
}
