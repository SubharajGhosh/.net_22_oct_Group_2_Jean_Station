namespace JeanStation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hddf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jeans", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jeans", "ImageUrl");
        }
    }
}
