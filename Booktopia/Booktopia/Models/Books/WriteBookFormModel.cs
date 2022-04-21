namespace Booktopia.Models.Books
{
    public class WriteBookFormModel
    {
        public string Title { get; set; }

        public string Annotation { get; set; }

        public int CategoryId { get; set; }

        public string ImageUrl { get; set; }
    }
}
