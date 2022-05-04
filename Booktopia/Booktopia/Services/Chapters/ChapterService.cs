namespace Booktopia.Services.Chapters
{
    using Booktopia.Data;
    using Booktopia.Data.Models;
    using Booktopia.Models.Chapters;
    using System.Linq;

    public class ChapterService : IChapterService
    {
        private readonly BooktopiaDbContext data;

        public ChapterService(BooktopiaDbContext data)
            => this.data = data;

        public ChapterViewModel ById(int id)
        {
            var userId = this.data
                .Books
                .Where(b => b.Chapters.Any(c => c.Id == id))
                .Select(b => b.Author.UserId)
                .FirstOrDefault();

            var chapterId = this.data
            .Chapters
            .Where(c => c.Id == id)
            .Select(c => new ChapterViewModel
            {
                Title = c.Title,
                Text = c.Text,
                UserId = userId
            })
            .FirstOrDefault();

            return chapterId;
        }

        public int Create(string title, string text, int bookId)
        {
            var chapterData = new Chapter
            {
                Title = title,
                Text = text,
                BookId = bookId
            };

            this.data.Chapters.Add(chapterData);
            this.data.SaveChanges();

            return chapterData.Id;
        }
    }
}
