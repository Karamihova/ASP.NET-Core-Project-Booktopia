namespace Booktopia.Services.Books
{
    using Booktopia.Models.Books;
    using System.Collections.Generic;

    public interface IBookService
    {
        IEnumerable<BookServiceModel> All();

        IEnumerable<BookServiceModel> ByAuthor(string userId);

        IEnumerable<BookServiceModel> ByCategoryType(string category);

        IEnumerable<BookCategoryServiceModel> AllCategories();

        BookDetailsServiceModel Details(int id);

        BookViewModel ById(int id);

        bool Delete(int id);

        BookChapterViewModel Chapters(int id);

        bool CategoryExists(int categoryId);

        bool IsByAuthor(int id, int authorId);

        int Create
            (
                string title,
                string annotation,
                string imageUrl,
                int categoryId,
                int authorId
            );

        bool Edit
            (
                int id,
                string title,
                string annotation,
                string imageUrl,
                int categoryId
            );
    }
}
