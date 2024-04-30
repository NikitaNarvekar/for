using BloggingPlatform2.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform2.Data
{
    public class BloggerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }



        public BloggerDbContext(DbContextOptions<BloggerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
