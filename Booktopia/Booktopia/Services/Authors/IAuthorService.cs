namespace Booktopia.Services.Authors
{
    public interface IAuthorService
    {
        public bool IsAuthor(string userId);

        public int IdByUser(string userId);
    }
}
