using Microsoft.EntityFrameworkCore;
using SimpleCQRS.Domain.Models;


namespace SimpleCQRS.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {}

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ignore certain properties/entities if necessary
            // modelBuilder.Ignore<SomeIgnoredEntity>();

            // Apply configurations from the assembly containing the DbContext
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

    }
}
