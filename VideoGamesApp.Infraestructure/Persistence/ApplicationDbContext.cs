using Microsoft.EntityFrameworkCore;
using VideoGamesApp.Domain.Entities;

namespace VideoGamesApp.Infraestructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<VideoGame> VideoGames => Set<VideoGame>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VideoGame>(builder =>
            {
                builder.HasKey(v => v.Id);
                builder.Property(v => v.Title).IsRequired().HasMaxLength(150);
                builder.Property(v => v.Price).HasPrecision(18, 2);
            });

            modelBuilder.Entity<Order>(builder =>
            {
                builder.HasKey(o => o.Id);
                builder.Property(o => o.CustomerEmail).IsRequired().HasMaxLength(200);

                builder.HasMany(o => o.Items)
                       .WithOne()
                       .HasForeignKey("OrderId")
                       .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderItem>(builder =>
            {
                builder.HasKey(i => i.Id);
                builder.Property(i => i.UnitPrice).HasPrecision(18, 2);
            });
        }
    }
}
