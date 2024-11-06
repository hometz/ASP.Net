using Microsoft.EntityFrameworkCore;
using Web_253501_Homets.Domain.Entities;

namespace Web_253501_Homets.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Dish>()
               .HasOne(d => d.Category)
               .WithMany(c => c.Dishes)
               .HasForeignKey(d => d.CategoryId);
        }
    }
}
