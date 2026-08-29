using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Data.ViewModels.Books
{
    public class BookForm : Form
    {
        /// <summary>
        /// Автор
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Год издания
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Оглавление
        /// </summary>
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string? Content { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public double Price { get; set; }

        public List<SelectListItem>? Authors { get; set; }

        public BookForm() { }

        public BookForm(int id, string? title, string? description, int authorId, int year,
            string? content, double price, List<SelectListItem> authors)
        {
            Id = id;
            Title = title;
            Description = description;
            AuthorId = authorId;
            Year = year;
            Content = content;
            Price = price;
            Authors = authors;
        }
    }
}