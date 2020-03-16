
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using GameStore.Data.Identity;
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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
