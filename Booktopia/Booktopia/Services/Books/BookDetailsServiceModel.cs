namespace Booktopia.Services.Books
{
    public class BookDetailsServiceModel : BookServiceModel
    {
        public int AuthorId { get; set; }

        public string UserId { get; set; }

        public int CategoryId { get; set; }
    }
}
