namespace Booktopia.Controllers
{
    using Booktopia.Data;
    using Booktopia.Data.Models;
    using Booktopia.Infrastructure;
    using Booktopia.Models.Authors;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class AuthorsController : Controller
    {
        private readonly BooktopiaDbContext data;

        public AuthorsController(BooktopiaDbContext data) 
            => this.data = data;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeAuthorFormModel author)
        {
            var userId = this.User.GetId();

            var isUserAuthor = this.data
                .Authors
                .Any(a => a.UserId == userId);

            if (isUserAuthor)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(author);
            }

            var authorData = new Author
            {
                Name = author.Name,
                Description = author.Description,
                UserId = userId
            };

            this.data.Authors.Add(authorData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Books");
        }
    }
}
