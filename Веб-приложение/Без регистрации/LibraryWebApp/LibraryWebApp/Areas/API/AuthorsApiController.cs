using Data.ViewModels.Authors;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace LibraryWebApp.Areas.API
{
    /// <summary>
    /// API-контроллер для работы с авторами
    /// </summary>
    [ApiController]
    [Route("api/Authors/[action]")]
    public class AuthorsApiController : Controller
    {
        private readonly AuthorService _authorService;

        public AuthorsApiController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        /// <summary>
        /// Получение авторов
        /// </summary>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="title">Название</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAuthors(int? pageNumber = null, int? pageSize = null, string? title = null)
            => new OkObjectResult(_authorService.BuildViewModelList(pageNumber ?? 1, pageSize ?? 10, title ?? "").Result);

        /// <summary>
        /// Создание автора
        /// </summary>
        /// <param name="form">Форма для создания автора</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(AuthorForm form)
        {
            var result = _authorService.Create(form).Result;

            return result.Item1 ? new OkObjectResult(result) : new BadRequestObjectResult(result.Item2);
        }

        /// <summary>
        /// Получение автора по Id
        /// </summary>
        /// <param name="id">Id автора</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetById(int id) => new OkObjectResult(_authorService.BuildFormById(id));

        /// <summary>
        /// Редактирование автора
        /// </summary>
        /// <param name="form">Форма для редактирования автора</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Update(AuthorForm form)
        {
            var result = _authorService.Update(form).Result;

            return result.Item1 ? new OkObjectResult(result) : new BadRequestObjectResult(result.Item2);
        }

        /// <summary>
        /// Удаление автора
        /// </summary>
        /// <param name="id">Id автора</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _authorService.Delete(id).Result;

            return result.Item1 ? new OkObjectResult(result) : new BadRequestObjectResult(result.Item2);
        }
    }
}