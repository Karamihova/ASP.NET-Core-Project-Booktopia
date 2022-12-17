namespace Booktopia.Services.Quotes
{
    using System.Collections.Generic;

    public interface IQuoteService
    {
        int Create(string text, string bookTitle);

        IEnumerable<QuoteServiceModel> All();
    }
}
