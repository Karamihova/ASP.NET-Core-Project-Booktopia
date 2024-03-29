﻿namespace Booktopia.Controllers
{
    using Booktopia.Infrastructure;
    using Booktopia.Models.Books;
    using Booktopia.Services.Authors;
    using Booktopia.Services.Books;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class BooksController : Controller
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;
        public BooksController(IBookService bookService, IAuthorService authorService)
        {
            this.bookService = bookService;
            this.authorService = authorService;
        }

        public IActionResult All()
        {
            var books = this.bookService.All();

            return View(books);
        }

        [Authorize]
        public IActionResult Write()
        {
            if (!authorService.IsAuthor(this.User.GetId()))
            {
                return RedirectToAction(nameof(AuthorsController.Become), "Authors");
            }

            return View(new BookFormModel
            {
                Categories = this.bookService.AllCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Write(BookFormModel book)
        {
            var authorId = this.authorService.IdByUser(this.User.GetId());

            if (authorId == 0)
            {
                return RedirectToAction(nameof(AuthorsController.Become), "Authors");
            }

            if (!bookService.CategoryExists(book.CategoryId))
            {
                ModelState.AddModelError(nameof(book.CategoryId), "Category type is not valid.");
            }

            if (!ModelState.IsValid)
            {
                book.Categories = this.bookService.AllCategories();
                return View(book);
            }

            this.bookService
                .Create
                (book.Title,
                book.Annotation,
                book.ImageUrl,
                book.CategoryId,
                authorId);

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
            var bookData = this.bookService.ById(id);

            if (bookData == null)
            {
                return BadRequest();
            }

            return View(bookData);
        }
        
        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!authorService.IsAuthor(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(AuthorsController.Become), "Authors");
            }

            var book = this.bookService.Details(id);
            if (book.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(new BookFormModel
            {
                Title = book.Title,
                Annotation = book.Annotation,
                ImageUrl=book.ImageUrl,
                CategoryId = book.CategoryId,
                Categories = this.bookService.AllCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, BookFormModel book)
        {
            var authorId = this.authorService.IdByUser(this.User.GetId());

            if (authorId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(AuthorsController.Become), "Authors");
            }

            if (!bookService.CategoryExists(book.CategoryId))
            {
                ModelState.AddModelError(nameof(book.CategoryId), "Category type is not valid.");
            }

            if (!ModelState.IsValid)
            {
                book.Categories = this.bookService.AllCategories();
                return View(book);
            }

            if (!this.bookService.IsByAuthor(id, authorId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            var bookIsEdited = this.bookService.Edit(
                id,
                book.Title,
                book.Annotation,
                book.ImageUrl,
                book.CategoryId);

            if (!bookIsEdited)
            {
                return BadRequest();
            }

            return RedirectToAction("ById", "Books", new {id = id});
        }

        public IActionResult Chapters(int id)
        {
            var bookChapters = this.bookService.Chapters(id);

            if (bookChapters == null)
            {
                return BadRequest();
            }

            return View(bookChapters);
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var authorId = this.authorService.IdByUser(this.User.GetId());
            var isAuthorOfBook = this.authorService.IsAuthorOfBook(authorId, id);


            if (!isAuthorOfBook && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var bookIsDeleted = this.bookService.Delete(id);

            if (!bookIsDeleted)
            {
                return BadRequest();
            }

            if (isAuthorOfBook)
            {
                return RedirectToAction(nameof(ByAuthor));
            }
            else
            {
                return RedirectToAction(nameof(All));
            }

        }

    }
}
