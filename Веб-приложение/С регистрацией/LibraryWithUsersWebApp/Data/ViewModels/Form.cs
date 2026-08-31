using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels
{
    /// <summary>
    /// Абстрактный класс формы
    /// </summary>
    public abstract class Form
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [Required(ErrorMessage = "Обязательное поле")]
        public string? Title { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }
    }
}
