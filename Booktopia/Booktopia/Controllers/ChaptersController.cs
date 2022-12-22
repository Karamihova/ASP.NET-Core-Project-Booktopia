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

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();
            var authorId = this.authorService.IdByUser(userId);
            if (!authorService.IsAuthorOfBook(authorId, id) && !User.IsAdmin())
            {
                return BadRequest();
            }

            var chapter = this.chapterService.ById(id);
            return View(new ChapterFormModel
            {
                Title = chapter.Title,
                Text = chapter.Text,
                BookId = chapter.BookId
            });

        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, ChapterFormModel chapter)
        {
            var authorId = this.authorService.IdByUser(this.User.GetId());

            if (authorId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(AuthorsController.Become), "Authors");
            }

            if (!authorService.IsAuthorOfBook(authorId, id) && !User.IsAdmin())
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(chapter);
            }

            var chapterIsEdited = this.chapterService.Edit(id, chapter.Text, chapter.Title);
            if (!chapterIsEdited)
            {
                return BadRequest();
            }

            return RedirectToAction("ById", "Chapters", new { id = id });
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var authorId = this.authorService.IdByUser(this.User.GetId());
            var bookId = this.chapterService.FindBookId(id);
            var isAuthorOfBook = this.authorService.IsAuthorOfBook(authorId, bookId);


            if (!isAuthorOfBook && !User.IsAdmin())
            {
                return Unauthorized();
            }


            var chapterIsDeleted = this.chapterService.Delete(id);
            if (!chapterIsDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction("Chapters", "Books", new {id = bookId});
        }

    }
}
