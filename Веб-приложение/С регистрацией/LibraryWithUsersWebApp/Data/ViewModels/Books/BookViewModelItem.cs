using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels.Books
{
    public class BookViewModelItem : ViewModelItem
    {
        /// <summary>
        /// Автор
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// Год издания
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Оглавление
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public string? User { get; set; }
    }
}