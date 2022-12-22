namespace Booktopia.Models.Chapters
{
    public class ChapterViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        public int BookId { get; set; }
    }
}
