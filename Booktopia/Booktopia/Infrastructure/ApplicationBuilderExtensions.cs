namespace Booktopia.Infrastructure
{
    using Booktopia.Data;
    using Booktopia.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var data = scopedServices.ServiceProvider.GetService<BooktopiaDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(BooktopiaDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category{Type = "Romance"},
                new Category{Type = "Short strory"},
                new Category{Type = "Historical"},
                new Category{Type = "Fiction"},
                new Category{Type = "Adventure"},
                new Category{Type = "Fantasy"}
            });

            data.SaveChanges();
        }
    }
}
