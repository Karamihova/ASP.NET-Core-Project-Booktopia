namespace Booktopia.Services.Chapters
{
    public interface IChapterService
    {
        int Create(string title, string text, int bookId);
    }
}
