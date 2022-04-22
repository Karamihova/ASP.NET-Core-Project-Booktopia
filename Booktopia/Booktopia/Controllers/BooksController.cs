namespace Booktopia.Controllers
{
    using Booktopia.Data;
    using Booktopia.Models.Books;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class BooksController : Controller
    {
        private readonly BooktopiaDbContext data;

        public BooksController(BooktopiaDbContext data) => this.data = data;

        public IActionResult All() => View();

        public IActionResult Write() => View(new WriteBookFormModel
        {
            Categories = this.GetCategories()
        });

        [HttpPost]
        public IActionResult Write(WriteBookFormModel book)
        {
            if (!ModelState.IsValid)
            {
                book.Categories = this.GetCategories();
                return View(book);
            }

            return RedirectToAction("All", "Books");
        }

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
