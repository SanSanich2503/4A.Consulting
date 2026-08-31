using Data.ViewModels.Authors;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace LibraryWebApp.Controllers
{
    /// <summary>
    /// Контроллер для работы с авторами
    /// </summary>
    public class AuthorsController : Controller
    {
        private readonly AuthorService _authorService; // Сервис для работы с авторами

        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        /// <summary>
        /// Отображение списка авторов
        /// </summary>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="name">ФИО автора</param>
        /// <returns></returns>
        public IActionResult Index(int pageNumber = 1, int pageSize = 10, string name = "")
            => View(_authorService.BuildViewModelList(pageNumber, pageSize, name).Result);

        /// <summary>
        /// Получение формы для создания автора
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ViewBag.ActionName = "Создание";
            ViewBag.MethodName = "Create";

            return View("CreateUpdate", _authorService.BuildForm());
        }

        /// <summary>
        /// Создание автора
        /// </summary>
        /// <param name="form">Форма автора</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(AuthorForm form)
        {
            if (ModelState.IsValid)
            {
                await _authorService.Create(form);

                return RedirectToAction("Index");
            }

            ViewBag.ActionName = "Создание";
            ViewBag.MethodName = "Create";

            return View("CreateUpdate", _authorService.BuildByForm(form));
        }

        /// <summary>
        /// Получение формы для редактирования автора
        /// </summary>
        /// <param name="id">Id автора</param>
        /// <returns></returns>
        public IActionResult Update(int id)
        {
            ViewBag.ActionName = "Редактирование";
            ViewBag.MethodName = "Update";

            return View("CreateUpdate", _authorService.BuildFormById(id));
        }

        /// <summary>
        /// Редактирование автора
        /// </summary>
        /// <param name="form">Форма автора</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(AuthorForm form)
        {
            if (ModelState.IsValid)
            {
                await _authorService.Update(form);

                return RedirectToAction("Index");
            }

            ViewBag.ActionName = "Редактирование";
            ViewBag.MethodName = "Update";

            return View("CreateUpdate", _authorService.BuildByForm(form));
        }

        /// <summary>
        /// Удаление автора
        /// </summary>
        /// <param name="id">Id автора</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.Delete(id);

            return Content("OK");
        }
    }
}