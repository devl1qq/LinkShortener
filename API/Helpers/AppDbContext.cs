using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using API.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{   
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }
        public IConfiguration Configuration { get; }

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "admin",
                Password = "admin@123",
                Role = "admin"
            });

            base.OnModelCreating(modelBuilder);
        }

    }

}
