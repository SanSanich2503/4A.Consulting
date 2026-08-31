using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels
{
    /// <summary>
    /// Класс элемента списка
    /// </summary>
    public class ViewModelItem
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }
    }
}