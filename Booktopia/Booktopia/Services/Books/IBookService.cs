namespace Booktopia.Services.Books
{
    using System.Collections.Generic;

    public interface IBookService
    {
        IEnumerable<BookServiceModel> All();
        IEnumerable<BookServiceModel> ByAuthor(string userId);
    }
}
