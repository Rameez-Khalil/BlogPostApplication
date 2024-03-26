using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    //Since EF core knows about the DbContext
    public class BloggieDbContext : DbContext
    {
        //We will overrie this options parameters from program file.
        public BloggieDbContext(DbContextOptions options) : base(options)
        {
        }

        //Notate the props that you want in database.
        public DbSet<BlogPost> BlogPosts{ get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
