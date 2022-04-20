namespace Booktopia.Data
{
    using Booktopia.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class BooktopiaDbContext : IdentityDbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Chapter> Chapters { get; set; }

        public DbSet<Quote> Quotes { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public BooktopiaDbContext(DbContextOptions<BooktopiaDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
