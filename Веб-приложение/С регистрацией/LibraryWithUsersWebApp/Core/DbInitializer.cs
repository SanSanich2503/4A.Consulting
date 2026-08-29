using Core.Entities.Authors;
using Core.Entities.Roles;
using Core.Entities.Users;
using System.Security.Cryptography;
using System.Text;

namespace Core
{
    public static class DbInitializer
    {
        public static async Task Initialize(DataContext context)
        {
            var currentDateTime = DateTime.Now;

            await CreateRoles(context, currentDateTime);
            await CreateUsers(context, currentDateTime);
            await CreateAuthors(context, currentDateTime);
        }

        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        private static async Task CreateRoles(DataContext context, DateTime currentDateTime)
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(new List<Role>
            {
                new Role
                {
                    Title = "Админ",
                    Description = "Роль администратора.",
                    LastModified = currentDateTime
                },
                new Role
                {
                    Title = "Читатель",
                    Description = "Роль читателя.",
                    LastModified = currentDateTime
                }
            });
                await context.SaveChangesAsync();
            }
        }

        private static async Task CreateUsers(DataContext context, DateTime currentDateTime)
        {
            if (!context.Users.Any())
            {
                var adminRole = context.Roles.FirstOrDefault(x => x.Title == "Админ");
                if (adminRole != null)
                {
                    context.Users.Add(new User
                    {
                        UserGuid = Guid.NewGuid().ToString(),
                        Title = "Главный админ",
                        Description = "Главный админ системы.",
                        Email = "admin@admin.com",
                        Password = HashPassword("123"),
                        RoleId = adminRole.Id,
                        LastModified = currentDateTime
                    });
                    await context.SaveChangesAsync();
                }
            }
        }

        private static async Task CreateAuthors(DataContext context, DateTime currentDateTime)
        {
            if (!context.Authors.Any())
            {
                context.Authors.AddRange(new List<Author>
                {
                    new Author
                    {
                        Title = "Пушкин Александр Сергеевич",
                        Description = "Русский поэт, драматург и прозаик, заложивший основы русского реалистического направления, литературный критик и теоретик литературы, " +
                        "историк, публицист, журналист, редактор и издатель. Один из самых авторитетных литературных деятелей первой трети XIX века.",
                        LastModified = currentDateTime
                    },
                    new Author
                    {
                        Title = "Толстой Лев Николаевич",
                        Description = "Один из наиболее известных русских писателей и мыслителей, один из величайших в мире писателей‑романистов.",
                        LastModified = currentDateTime
                    },
                    new Author
                    {
                        Title = "Достоевский Фёдор Михайлович",
                        Description = "Русский писатель, мыслитель, философ и публицист. Член-корреспондент Петербургской академии наук с 1877 года.",
                        LastModified = currentDateTime
                    }
                });
                await context.SaveChangesAsync();
            }
        }
    }
}