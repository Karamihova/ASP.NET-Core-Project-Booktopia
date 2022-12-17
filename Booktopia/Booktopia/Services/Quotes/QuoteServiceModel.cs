namespace Booktopia.Services.Quotes
{
    public class QuoteServiceModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string BookTitle { get; set; }

        public string BookAuthor { get; set; }

        public string BookImage { get; set; }
    }
}
