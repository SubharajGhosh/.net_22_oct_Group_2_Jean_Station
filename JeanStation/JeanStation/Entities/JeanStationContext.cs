using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace JeanStation.Entities
{
    public class JeanStationContext : DbContext
    {
    public JeanStationContext() : base("JeanStationConnection") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Jeans> Jeans { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PaymentDetails> PaymentDetailss { get; set; }
        public DbSet<Shopkeeper> Shopkeepers { get; set; }

    }
}