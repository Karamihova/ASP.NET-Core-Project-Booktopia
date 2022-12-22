namespace Booktopia.Services.Quotes
{
    using Booktopia.Data;
    using Booktopia.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class QuoteService : IQuoteService
    {
        private readonly BooktopiaDbContext data;


        public QuoteService(BooktopiaDbContext data)
        {
            this.data = data;
        }

        public int Create(string text, string bookTitle)
        {
            var searchedBook = this.data.Books.FirstOrDefault(b => b.Title == bookTitle);
           
            if(searchedBook == null)
            {
                return -1;
            }

            var bookId = searchedBook.Id;
            var book = this.data.Books.Find(bookId);

            var quoteData = new Quote
            {
                Text = text,
                BookId = book.Id
            };

            this.data.Quotes.Add(quoteData);
            this.data.SaveChanges();

            return quoteData.Id;
        }

        private static IEnumerable<QuoteServiceModel> GetQuotes(IQueryable<Quote> quoteQuery)
            => quoteQuery
                .Select(q => new QuoteServiceModel
                {
                    Id = q.Id,
                    Text = q.Text,
                    BookAuthor = q.Book.Author.Name,
                    BookTitle = q.Book.Title,
                    BookImage = q.Book.ImageUrl
                })
                .ToList();

        public IEnumerable<QuoteServiceModel> All()
            => GetQuotes(this.data.Quotes);
    }
}
