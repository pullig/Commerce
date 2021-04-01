using Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Infrastructure.Context
{
    public class CommerceContext : DbContext
    {
        public CommerceContext(DbContextOptions<CommerceContext> options)
            : base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.HasKey(o => o.Id);

                entity.HasOne(o => o.User).
                    WithOne().
                    HasForeignKey<Order>(o => o.UserId);

                entity.HasMany(o => o.Products).
                    WithOne().
                    HasForeignKey(o => o.OrderId);

                entity.Navigation(o => o.Products)
                    .UsePropertyAccessMode(PropertyAccessMode.Property);
            });

            modelBuilder.Entity<ProductOrder>((entity =>
            {
                entity.ToTable("ProductOrder");

                entity.HasOne(o => o.Product).
                    WithOne();

                entity.HasKey(o => o.Id);

                entity.Property(po => po.UnityPrice)
                    .HasColumnType("DECIMAL(10,2)");
            }));

            modelBuilder.Entity<Product>((entity =>
            {
                entity.ToTable("Product");

                entity.HasKey(o => o.Id);

                entity.Property(o => o.Price)
                    .HasColumnType("DECIMAL(10,2)");

                entity.Property(o => o.Name)
                    .HasMaxLength(50);

                entity.Property(o => o.Description)
                    .HasMaxLength(250);
            }));

            modelBuilder.Entity<User>((entity =>
            {
                entity.ToTable("User");

                entity.HasKey(o => o.Id);

                entity.Property(o => o.Username)
                    .HasMaxLength(50);

                entity.Property(o => o.DisplayName)
                    .HasMaxLength(250);
            }));

        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductOrder> ProductOrders { get; set; }
    }
}
