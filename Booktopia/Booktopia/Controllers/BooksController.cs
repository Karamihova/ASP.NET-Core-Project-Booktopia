namespace Booktopia.Controllers
{
    using Booktopia.Data;
    using Booktopia.Data.Models;
    using Booktopia.Infrastructure;
    using Booktopia.Models.Books;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class BooksController : Controller
    {
        private readonly BooktopiaDbContext data;

        public BooksController(BooktopiaDbContext data) 
            => this.data = data;

        public IActionResult All()
        {
            var books = this.data
                .Books
                .OrderByDescending(x => x.CreatedOn)
                .Select(b => new BookListingViewModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Annotation = b.Annotation.Substring(0, 200),
                        ImageUrl = b.ImageUrl,
                        Category = b.Category.Type,
                        Author = b.Author.Name
                    })
                .ToList();

            return View(books);
        }

        [Authorize]
        public IActionResult Write()
        {
            if (!UserIsAuthor())
            {
                return RedirectToAction(nameof(AuthorsController.Become), "Authors");
            }

            return View(new WriteBookFormModel
            {
                Categories = this.GetCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Write(WriteBookFormModel book)
        {
            var authorId = this.data
                .Authors
                .Where(a => a.UserId == this.User.GetId())
                .Select(a => a.Id)
                .FirstOrDefault();

            if (authorId == 0)
            {
                return RedirectToAction(nameof(AuthorsController.Become), "Authors");
            }

            if (!data.Categories.Any(c => c.Id == book.CategoryId))
            {
                ModelState.AddModelError(nameof(book.CategoryId), "Category type is not valid.");
            }

            if (!ModelState.IsValid)
            {
                book.Categories = this.GetCategories();
                return View(book);
            }

            var bookData = new Book
            {
                Title = book.Title,
                Annotation = book.Annotation,
                ImageUrl = book.ImageUrl,
                CategoryId = book.CategoryId,
                AuthorId = authorId
            };

            this.data.Books.Add(bookData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private bool UserIsAuthor()
            => this.data
                .Authors
                .Any(a => a.UserId == this.User.GetId());

        private ICollection<BookCategoryViewModel> GetCategories()
            => this.data
                .Categories
                .Select(c => new BookCategoryViewModel
                {
                    Id = c.Id,
                    Type = c.Type
                }).ToList();
        
    }
}
