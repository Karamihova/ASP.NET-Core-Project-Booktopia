namespace Booktopia.Services.Quotes
{
    public interface IQuoteService
    {
        int Create(string text, string bookTitle);
    }
}
