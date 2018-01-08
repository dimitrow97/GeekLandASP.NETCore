using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PCStore.Data.Models;

namespace PCStore.Data
{
    public class PCStoreDbContext : IdentityDbContext<User>
    {
        public PCStoreDbContext(DbContextOptions<PCStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().ToTable("Products");
            builder.Entity<ProductBuyer>()
            .HasKey(t => new { t.ProductId, t.UserId });

            builder
               .Entity<ProductBuyer>()
               .HasOne(pc => pc.Product)
               .WithMany(p => p.Buyers)
               .HasForeignKey(i => i.ProductId);

            builder
               .Entity<ProductBuyer>()
               .HasOne(o => o.User)
               .WithMany(i => i.ProductsBougth)
               .HasForeignKey(i => i.UserId);

            base.OnModelCreating(builder);
        }
    }
}
