namespace Booktopia.Controllers
{
    using Booktopia.Infrastructure;
    using Booktopia.Models.Chapters;
    using Booktopia.Services.Authors;
    using Booktopia.Services.Chapters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ChaptersController : Controller
    {
        private readonly IAuthorService authorService;
        private readonly IChapterService chapterService;

        public ChaptersController(IAuthorService authorService, IChapterService chapterService)
        {
            this.authorService = authorService;
            this.chapterService = chapterService;
        }

        [Authorize]
        public IActionResult Write(int bookId)
        {
            int authorId = this.authorService.IdByUser(this.User.GetId());

            if (authorId == 0)
            {
                return BadRequest();
            }

            if (!authorService.IsAuthorOfBook(authorId, bookId))
            {
                return BadRequest();
            }

            return View(new ChapterFormModel
            {
                BookId = bookId
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Write(ChapterFormModel chapter)
        {
            if (!ModelState.IsValid)
            {
                return View(chapter);
            }

            this.chapterService
                .Create
                (chapter.Title,
                chapter.Text,
                chapter.BookId
                );

            return RedirectToAction("Chapters", "Books", new {id = chapter.BookId});
        }

        [Authorize]
        public IActionResult ById(int id)
        {
            var chapterData = chapterService.ById(id);

            if (chapterData == null)
            {
                return BadRequest();
            }

            return View(chapterData);
        }

        //public IActionResult Edit(int id)
        //public IActionResult Delete(int id)

    }
}
