namespace Booktopia.Services.Chapters
{
    using Booktopia.Models.Chapters;

    public interface IChapterService
    {
        int Create(string title, string text, int bookId);

        ChapterViewModel ById(int id);
    }
}
