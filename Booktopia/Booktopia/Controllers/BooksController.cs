namespace Booktopia.Controllers
{
    using Booktopia.Data;
    using Booktopia.Data.Models;
    using Booktopia.Infrastructure;
    using Booktopia.Models.Books;
    using Booktopia.Services.Books;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class BooksController : Controller
    {
        private readonly BooktopiaDbContext data;
        private readonly IBookService bookService;
        public BooksController(BooktopiaDbContext data, IBookService bookService)
        {
            this.data = data;
            this.bookService = bookService;
        }

        public IActionResult All()
        {
            var books = this.bookService.All();

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

        public IActionResult ByAuthor()
        {
            var authorBooks = this.bookService.ByAuthor(User.GetId());

            return View(authorBooks);
        }

        [Route("Books/ByCategoryType/{category}")]
        public IActionResult ByCategoryType(string category)
        {
            var booksByCategory = this.bookService.ByCategoryType(category);

            return View(booksByCategory);
        }

        [Route("Books/ById/{id:int}")]
        public IActionResult ById(int id)
        {
            var book = this.data.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return BadRequest();
            }

            var bookData = this.data
                .Books
                .Where(b => b.Id == id)
                .Select(b => new BookViewModel
                {
                    Id = b.Id,
                    Title=b.Title,
                    Annotation=b.Annotation,
                    ImageUrl =b.ImageUrl,
                    CategoryType = b.Category.Type,
                    AuthorName = b.Author.Name,
                    ChaptersCount = b.Chapters.Count() == null ? 0 : b.Chapters.Count(),
                    ReviewsCount = b.Reviews.Count() == null ? 0 : b.Reviews.Count(),
                    Rating = b.Reviews.Average(r => r.Rating) == null ? 0 : b.Reviews.Average(r => r.Rating),
                    QuotesCount = b.Quotes.Count() == null ? 0 : b.Quotes.Count()
                })
                .FirstOrDefault();

            return View(bookData);
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
