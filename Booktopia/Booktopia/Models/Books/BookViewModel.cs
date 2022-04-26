namespace Booktopia.Models.Books
{
    public class BookViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Annotation { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryType { get; set; }

        public string AuthorName { get; set; }

        public int ChaptersCount { get; set; }

        public int ReviewsCount { get; set; }

        public double Rating { get; set; }

        public int QuotesCount { get; set; }
    }
}
