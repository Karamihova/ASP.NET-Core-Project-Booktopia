namespace Booktopia.Services.Home
{
    using Booktopia.Data;
    using System.Linq;

    public class HomeService : IHomeService
    {
        private readonly BooktopiaDbContext data;

        public HomeService(BooktopiaDbContext data)
        {
            this.data = data;
        }

        public HomeInfoServiceModel GetInfoFromDatabase()
        {
            var usersCount = data.Users.Count();
            var authorsCount = data.Authors.Count();
            var booksCount = data.Books.Count();

            return new HomeInfoServiceModel
            {
                AuthorsCount = authorsCount,
                UsersCount = usersCount,
                BooksCount = booksCount
            };
        }
    }
}
