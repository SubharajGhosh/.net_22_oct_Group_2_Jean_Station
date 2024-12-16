namespace JeanStation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdb12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jeans", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.PaymentDetails", "TotalAmount", c => c.Double(nullable: false));
            AddColumn("dbo.PaymentDetails", "PaymentDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OrderItems", "UnitPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.OrderItems", "TotalPrice", c => c.Double(nullable: false));
            DropColumn("dbo.Jeans", "imageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jeans", "imageUrl", c => c.String());
            AlterColumn("dbo.OrderItems", "TotalPrice", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderItems", "UnitPrice", c => c.Int(nullable: false));
            DropColumn("dbo.PaymentDetails", "PaymentDate");
            DropColumn("dbo.PaymentDetails", "TotalAmount");
            DropColumn("dbo.Jeans", "Price");
        }
    }
}
