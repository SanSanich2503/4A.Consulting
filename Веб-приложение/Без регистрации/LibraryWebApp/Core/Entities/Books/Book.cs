using Core.Entities.Authors;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Books
{
    public class Book : Entity
    {
        /// <summary>
        /// Id автора
        /// </summary>
        [DataType("ForeignKey")]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        [DataType("Reference")]
        public virtual Author Author { get; set; }

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
    }
}