namespace Booktopia.Services.Books
{
    public class BookServiceModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Annotation { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }

        public string AuthorName { get; set; }
    }
}
