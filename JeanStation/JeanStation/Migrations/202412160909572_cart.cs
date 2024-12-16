namespace JeanStation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.String(nullable: false, maxLength: 128),
                        JeansId = c.String(maxLength: 128),
                        Price = c.Double(nullable: false),
                        CustomerId = c.String(maxLength: 128),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Jeans", t => t.JeansId)
                .Index(t => t.JeansId)
                .Index(t => t.CustomerId);
            
            AlterColumn("dbo.Jeans", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.OrderItems", "UnitPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "JeansId", "dbo.Jeans");
            DropForeignKey("dbo.Carts", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Carts", new[] { "CustomerId" });
            DropIndex("dbo.Carts", new[] { "JeansId" });
            AlterColumn("dbo.OrderItems", "UnitPrice", c => c.Int(nullable: false));
            AlterColumn("dbo.Jeans", "Price", c => c.Int(nullable: false));
            DropTable("dbo.Carts");
        }
    }
}
