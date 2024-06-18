using FruitTemplate_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace FruitTemplate_BackEnd.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderInfo> SliderInfo { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<BestSeller> BestSellers { get; set; }
        public DbSet<Fact> Facts { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
