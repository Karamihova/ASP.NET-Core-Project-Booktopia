namespace Booktopia.Infrastructure
{
    using Booktopia.Data;
    using Booktopia.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static WebConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var serviceProvider = scopedServices.ServiceProvider;

            MigrateDatabase(serviceProvider);
            SeedCategories(serviceProvider);
            SeedAdministrator(serviceProvider);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider service)
        {
            var data = service.GetRequiredService<BooktopiaDbContext>();
            data.Database.Migrate();

        }

        private static void SeedCategories(IServiceProvider service)
        {
            var data = service.GetRequiredService<BooktopiaDbContext>();

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

        private static void SeedAdministrator(IServiceProvider service)
        {
            var userManager = service.GetRequiredService<UserManager<User>>();
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRole))
                    {
                        return;
                    }

                    var role = new IdentityRole
                    {
                        Name = AdministratorRole
                    };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@booktopia.com";
                    const string adminPassword = "admin123";
                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FullName = "Admin"
                    };

                    await userManager.CreateAsync(user, adminPassword);
                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
