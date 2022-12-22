namespace Booktopia.Services.Chapters
{
    using Booktopia.Models.Chapters;

    public interface IChapterService
    {
        int Create(string title, string text, int bookId);

        ChapterViewModel ById(int id);

        bool Edit
            (
                int id,
                string text,
                string title
            );

        bool Delete(int id);

        int FindBookId(int id);
    }
}
