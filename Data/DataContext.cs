using Microsoft.EntityFrameworkCore;
using WebApiSandbox.Models;

namespace WebApiSandbox.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductProducer> ProductProducers { get; set; }

        // many-to-many setuup:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasKey(pc => new { pc.ProductId, pc.CategoryId });
            modelBuilder.Entity<ProductCategory>().HasOne(p => p.Product).WithMany(pc => pc.ProductCategories).HasForeignKey(c => c.CategoryId);
            modelBuilder.Entity<ProductCategory>().HasOne(p => p.Category).WithMany(pc => pc.ProductCategories).HasForeignKey(c => c.ProductId);

            modelBuilder.Entity<ProductProducer>().HasKey(pp => new { pp.ProductId, pp.ProducerId });
            modelBuilder.Entity<ProductProducer>().HasOne(p => p.Product).WithMany(pp => pp.ProductProducers).HasForeignKey(p => p.ProducerId);
            modelBuilder.Entity<ProductProducer>().HasOne(p => p.Producer).WithMany(pp => pp.ProductProducers).HasForeignKey(p => p.ProductId);
        }

    }
}
