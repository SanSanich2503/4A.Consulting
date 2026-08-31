using Data.ViewModels.Books;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace LibraryWebApp.Controllers
{
    /// <summary>
    /// Контроллер для работы с книгами
    /// </summary>
    public class BooksController : Controller
    {
        private readonly BookService _bookService; // Сервис для работы с книгами

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Получение списка книг
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IActionResult Index(int pageNumber = 1, int pageSize = 10, string name = "")
            => View(_bookService.BuildViewModelList(pageNumber, pageSize, name).Result);

        /// <summary>
        /// Получение формы для создания книги
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ViewBag.ActionName = "Создание";
            ViewBag.MethodName = "Create";

            return View("CreateUpdate", _bookService.BuildForm());
        }

        /// <summary>
        /// Создание книги
        /// </summary>
        /// <param name="form">Форма книги</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(BookForm form)
        {
            if (ModelState.IsValid)
            {
                await _bookService.Create(form);

                return RedirectToAction("Index");
            }

            ViewBag.ActionName = "Создание";
            ViewBag.MethodName = "Create";

            return View("CreateUpdate", _bookService.BuildByForm(form));
        }

        /// <summary>
        /// Получение формы для создания книги
        /// </summary>
        /// <param name="id">Id книги</param>
        /// <returns></returns>
        public IActionResult Update(int id)
        {
            ViewBag.ActionName = "Редактирование";
            ViewBag.MethodName = "Update";

            return View("CreateUpdate", _bookService.BuildFormById(id));
        }

        /// <summary>
        /// Редактирование книги
        /// </summary>
        /// <param name="form">Форма книги</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(BookForm form)
        {
            if (ModelState.IsValid)
            {
                await _bookService.Update(form);

                return RedirectToAction("Index");
            }

            ViewBag.ActionName = "Редактирование";
            ViewBag.MethodName = "Update";

            return View("CreateUpdate", _bookService.BuildByForm(form));
        }

        /// <summary>
        /// Удаление книги
        /// </summary>
        /// <param name="id">Id книги</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.Delete(id);

            return Content("OK");
        }
    }
}