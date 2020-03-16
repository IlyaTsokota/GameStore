using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using Attribute = GameStore.Model.Attribute;

namespace GameStore.Data.ApplicationContext
{
    public sealed class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext() : base("name=GameStore")
        {
        }

        static ApplicationContext()
        {
            Database.SetInitializer(new ApplicationContextInitializer());
        }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Order> Order { get; set; }

        public IDbSet<OrderDetails> OrderDetails { get; set; }

        public IDbSet<OrderStatus> OrderStatus { get; set; }

        public IDbSet<Attribute> Attributes { get; set; }

        public IDbSet<AttributeValue> AttributeValues { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<ProductImage> ProductImages { get; set; }

        public IDbSet<Review> ProductReviews { get; set; }

        public IDbSet<Supply> ProductSupplies { get; set; }

        public IDbSet<Supplier> Suppliers { get; set; }

        public IDbSet<Supply> Supplies { get; set; }

        public IDbSet<User> User { get; set; }

        public IDbSet<Log> Logs { get; set; }

        public void Commit()
        {
            SaveChanges();
        }
    }
}
