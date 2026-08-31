using Data.ViewModels.Books;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace LibraryWebApp.Areas.API
{
    /// <summary>
    /// API-контроллер для работы с книгами
    /// </summary>
    [ApiController]
    [Route("api/Books/[action]")]
    public class BooksApiController : Controller
    {
        private readonly BookService _bookService;

        public BooksApiController(BookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Получение книг
        /// </summary>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="title">Название</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetBooks(int? pageNumber = null, int? pageSize = null, string? title = null)
            => new OkObjectResult(_bookService.BuildViewModelList(pageNumber ?? 1, pageSize ?? 10, title ?? "").Result);

        /// <summary>
        /// Создание книги
        /// </summary>
        /// <param name="form">Форма для создания книги</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(BookForm form)
        {
            var result = _bookService.Create(form).Result;

            return result.Item1 ? new OkObjectResult(result) : new BadRequestObjectResult(result.Item2);
        }

        /// <summary>
        /// Получение книги по Id
        /// </summary>
        /// <param name="id">Id книги</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetById(int id) => new OkObjectResult(_bookService.BuildFormById(id));

        /// <summary>
        /// Редактирование книги
        /// </summary>
        /// <param name="form">Форма для редактирования книги</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Update(BookForm form)
        {
            var result = _bookService.Update(form).Result;

            return result.Item1 ? new OkObjectResult(result) : new BadRequestObjectResult(result.Item2);
        }

        /// <summary>
        /// Удаление книги
        /// </summary>
        /// <param name="id">Id книги</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _bookService.Delete(id).Result;

            return result.Item1 ? new OkObjectResult(result) : new BadRequestObjectResult(result.Item2);
        }
    }
}