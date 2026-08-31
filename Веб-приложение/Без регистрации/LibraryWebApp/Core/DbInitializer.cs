using Core.Entities.Authors;

namespace Core
{
    /// <summary>
    /// Класс-инициализатор базы данных
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// Инициализация базы данных
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        /// <returns></returns>
        public static async Task Initialize(DataContext context)
        {
            var currentDateTime = DateTime.Now;

            await CreateAuthors(context, currentDateTime);
        }

        /// <summary>
        /// Создание авторов по умолчанию в базе данных
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        /// <param name="currentDateTime">Текущая дата и время</param>
        /// <returns></returns>
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