using E_commerceApp.Core.Entities;
using E_commerceApp.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Data.DAL
{
    public class WatchesDbContext : IdentityDbContext
    {
        public WatchesDbContext(DbContextOptions<WatchesDbContext> options) : base(options)
        {

        }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Size> Sizes { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductSize> ProductSizes { get; set; }

        public DbSet<ProductColor> ProductColors { get; set; }

        public DbSet<Slider> Sliders { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<BasketItem> BasketItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<ProductReview> ProductReviews { get; set; }

        public DbSet<UserContact> UserContacts { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<Feature> Features { get; set; }

        public DbSet<Info> Infos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BrandConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
