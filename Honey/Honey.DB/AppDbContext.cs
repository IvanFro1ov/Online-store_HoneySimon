using Honey.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace Honey.DB
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RestoringCodeEntity> RestoringCodes { get; set; }
        
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ShopCartEntity> ShopCarts { get; set; }
        
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductTypeEntity> ProductTypes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserEntity>(user =>
            {
                user.Property(u => u.Firstname).IsRequired().HasMaxLength(50);
                user.Property(u => u.Name).IsRequired().HasMaxLength(50);
                user.Property(u => u.Firstname).HasMaxLength(30);
                user.Property(u => u.Age).IsRequired();
                user.Property(u => u.Email).IsRequired().HasMaxLength(50);
                user.Property(u => u.Password).IsRequired().HasMaxLength(100);
                user.Property(u => u.UserName).IsRequired().HasMaxLength(50);
                user.Property(u => u.DateCreated).IsRequired();
            });

            builder.Entity<RestoringCodeEntity>(code =>
            {
                code.Property(c => c.Code).IsRequired().HasMaxLength(6);
                code.Property(c => c.Expiration).IsRequired();
                code.Property(u => u.DateCreated).IsRequired();
                code.Property(u => u.Email).IsRequired().HasMaxLength(50);
            });
        }
    }
}