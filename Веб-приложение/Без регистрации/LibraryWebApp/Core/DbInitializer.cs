using Core.Entities.Authors;

namespace Core
{
    public static class DbInitializer
    {
        public static async Task Initialize(DataContext context)
        {
            await CreateAuthors(context);
        }

        private static async Task CreateAuthors(DataContext context)
        {
            if (!context.Authors.Any())
            {
                context.Authors.AddRange(new List<Author>
                {
                    new Author
                    {
                        Title = "Пушкин Александр Сергеевич",
                        Description = "Русский поэт, драматург и прозаик, заложивший основы русского реалистического направления, литературный критик и теоретик литературы, " +
                        "историк, публицист, журналист, редактор и издатель. Один из самых авторитетных литературных деятелей первой трети XIX века."
                    },
                    new Author
                    {
                        Title = "Толстой Лев Николаевич",
                        Description = "Один из наиболее известных русских писателей и мыслителей, один из величайших в мире писателей‑романистов."
                    },
                    new Author
                    {
                        Title = "Достоевский Фёдор Михайлович",
                        Description = "Русский писатель, мыслитель, философ и публицист. Член-корреспондент Петербургской академии наук с 1877 года."
                    }
                });
                await context.SaveChangesAsync();
            }
        }
    }
}