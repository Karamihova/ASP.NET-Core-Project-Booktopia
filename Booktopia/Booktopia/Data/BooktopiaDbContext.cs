namespace Booktopia.Data
{
    using Booktopia.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class BooktopiaDbContext : IdentityDbContext
    {
        public BooktopiaDbContext(DbContextOptions<BooktopiaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Chapter> Chapters { get; set; }

        public DbSet<Quote> Quotes { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Chapter>()
                .HasOne(b => b.Book)
                .WithMany(c => c.Chapters)
                .HasForeignKey(b => b.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Quote>()
                .HasOne(b => b.Book)
                .WithMany(q => q.Quotes)
                .HasForeignKey(b => b.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Review>()
                .HasOne(b => b.Book)
                .WithMany(r => r.Reviews)
                .HasForeignKey(b => b.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Book>()
                .HasOne(c => c.Category)
                .WithMany(b => b.Books)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Book>()
                .HasOne(a => a.Author)
                .WithMany(b => b.WrittenBooks)
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Author>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Author>(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }


    }
}
