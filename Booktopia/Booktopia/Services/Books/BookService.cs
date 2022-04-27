namespace Booktopia.Services.Books
{
    using Booktopia.Data;
    using Booktopia.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class BookService : IBookService
    {
        private readonly BooktopiaDbContext data;

        public BookService(BooktopiaDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<BookServiceModel> All()
            => GetBooks(this.data
                .Books
                .OrderByDescending(x => x.CreatedOn));
                

        public IEnumerable<BookServiceModel> ByAuthor(string userId)
            => GetBooks(this.data
                .Books
                .Where(b => b.Author.UserId == userId));

        public IEnumerable<BookServiceModel> ByCategoryType(string category)
            => GetBooks(this.data
                .Books
                .Where(b => b.Category.Type == category));

        private static IEnumerable<BookServiceModel> GetBooks(IQueryable<Book> bookQuery)
            => bookQuery
                .Select(b => new BookServiceModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Annotation = b.Annotation.Substring(0, 200),
                    ImageUrl = b.ImageUrl,
                    Category = b.Category.Type,
                    Author = b.Author.Name
                })
                .ToList();

    }
}
