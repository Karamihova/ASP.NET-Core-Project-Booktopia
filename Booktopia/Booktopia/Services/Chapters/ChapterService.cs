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
                Id = id,
                Title = c.Title,
                Text = c.Text,
                UserId = userId,
                BookId = c.BookId
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

            var book = this.data.Books.Find(bookId);
            book.Chapters.Add(chapterData);

            this.data.Chapters.Add(chapterData);
            this.data.SaveChanges();

            return chapterData.Id;
        }

        public bool Delete(int id)
        {
            var chapter = this.data.Chapters.Find(id);
            if(chapter == null)
            {
                return false;
            }

            this.data.Chapters.Remove(chapter);
            this.data.SaveChanges();
            return true;
        }

        public bool Edit(int id, string text, string title)
        {
            var chapterData = this.data.Chapters.Find(id);

            if(chapterData == null)
            {
                return false;
            }

            chapterData.Text = text;
            chapterData.Title = title;
            this.data.SaveChanges();

            return true;
        }

        public int FindBookId(int id)
        {
            var chapter = this.data.Chapters.Find(id);

            if(chapter == null)
            {
                return 0;
            }

            return chapter.BookId;
        }
    }
}
