namespace JeanStation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdb2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderItems", "UnitPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderItems", "UnitPrice", c => c.Int(nullable: false));
        }
    }
}
