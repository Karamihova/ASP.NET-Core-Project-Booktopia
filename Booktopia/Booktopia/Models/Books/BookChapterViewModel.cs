namespace Booktopia.Models.Books
{
    using Booktopia.Services.Books;
    using System.Collections.Generic;

    public class BookChapterViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorName { get; set; }

        public IEnumerable<BookChapterServiceModel> Chapters { get; set; }
    }
}
