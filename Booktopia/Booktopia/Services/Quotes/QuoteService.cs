namespace Booktopia.Services.Quotes
{
    using Booktopia.Data;
    using Booktopia.Data.Models;
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
            var book = data.Books.Where(b => b.Title == bookTitle).FirstOrDefault();
            if(book == null)
            {
                return -1;
            }

            var quoteData = new Quote
            {
                Text = text,
                BookId = book.Id
            };

            this.data.Quotes.Add(quoteData);
            this.data.SaveChanges();

            return quoteData.Id;
        }
    }
}
