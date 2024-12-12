namespace JeanStation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JeanDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandId = c.String(nullable: false, maxLength: 128),
                        BrandName = c.String(),
                    })
                .PrimaryKey(t => t.BrandId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        CustomerName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Jeans",
                c => new
                    {
                        JeansId = c.String(nullable: false, maxLength: 128),
                        BrandId = c.String(maxLength: 128),
                        Type = c.String(),
                        Color = c.String(),
                        Size = c.String(),
                        Stock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JeansId)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderItemId = c.String(nullable: false, maxLength: 128),
                        Quantity = c.Int(nullable: false),
                        OrderId = c.String(maxLength: 128),
                        UnitPrice = c.Int(nullable: false),
                        TotalPrice = c.Int(nullable: false),
                        JeansId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderItemId)
                .ForeignKey("dbo.Jeans", t => t.JeansId)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .Index(t => t.OrderId)
                .Index(t => t.JeansId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.String(nullable: false, maxLength: 128),
                        CustomerId = c.String(maxLength: 128),
                        OrderDate = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        OrderStatus = c.String(),
                        PaymentStatus = c.String(),
                        City = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.PaymentDetails",
                c => new
                    {
                        PaymentId = c.String(nullable: false, maxLength: 128),
                        PaymentMode = c.String(),
                        PaymentStatus = c.String(),
                        OrderId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Shopkeepers",
                c => new
                    {
                        ShopkeeperId = c.String(nullable: false, maxLength: 128),
                        ShopName = c.String(),
                        Location = c.String(),
                        Address = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ShopkeeperId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shopkeepers", "UserId", "dbo.Users");
            DropForeignKey("dbo.PaymentDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.OrderItems", "JeansId", "dbo.Jeans");
            DropForeignKey("dbo.Jeans", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Customers", "UserId", "dbo.Users");
            DropIndex("dbo.Shopkeepers", new[] { "UserId" });
            DropIndex("dbo.PaymentDetails", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.OrderItems", new[] { "JeansId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.Jeans", new[] { "BrandId" });
            DropIndex("dbo.Customers", new[] { "UserId" });
            DropTable("dbo.Shopkeepers");
            DropTable("dbo.PaymentDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Jeans");
            DropTable("dbo.Users");
            DropTable("dbo.Customers");
            DropTable("dbo.Brands");
        }
    }
}
