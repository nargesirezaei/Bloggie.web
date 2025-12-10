using Bloggie.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Data
{
    public class BloggieDbContext : DbContext
    {
        public BloggieDbContext(DbContextOptions<BloggieDbContext> options): base(options)
        {
            //later on overwrite these DbContextOptions from program.cs. and we want to pass these options to the base class and
            ////hence we are creating this constructor with dbContext options parameters 

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogPost> BloggPosts { get; set; }

    }
}
