namespace JeanStation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cdb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentDetails", "TotalAmount", c => c.Double(nullable: false));
            AddColumn("dbo.PaymentDetails", "PaymentDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Jeans", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.OrderItems", "UnitPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.OrderItems", "TotalPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderItems", "TotalPrice", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderItems", "UnitPrice", c => c.Int(nullable: false));
            AlterColumn("dbo.Jeans", "Price", c => c.Int(nullable: false));
            DropColumn("dbo.PaymentDetails", "PaymentDate");
            DropColumn("dbo.PaymentDetails", "TotalAmount");
        }
    }
}
