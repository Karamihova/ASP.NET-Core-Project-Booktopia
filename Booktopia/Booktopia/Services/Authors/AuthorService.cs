namespace Booktopia.Services.Authors
{
    using Booktopia.Data;
    using System.Linq;

    public class AuthorService : IAuthorService
    {
        private readonly BooktopiaDbContext data;

        public AuthorService(BooktopiaDbContext data) 
            => this.data = data;

        public bool IsAuthor(string userId) 
            => this.data
            .Authors
            .Any(a => a.UserId == userId);
    }
}
