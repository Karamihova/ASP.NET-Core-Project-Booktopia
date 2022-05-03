namespace Booktopia.Services.Chapters
{
    using Booktopia.Data;
    using Booktopia.Data.Models;

    public class ChapterService : IChapterService
    {
        private readonly BooktopiaDbContext data;

        public ChapterService(BooktopiaDbContext data)
        {
            this.data = data;
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
