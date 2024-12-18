namespace JeanStation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cart1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Jeans", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jeans", "ImageUrl", c => c.String());
        }
    }
}
