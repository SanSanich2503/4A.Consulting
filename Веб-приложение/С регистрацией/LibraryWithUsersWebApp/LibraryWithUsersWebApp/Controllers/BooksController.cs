using Data.ViewModels.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace LibraryWebApp.Controllers
{
    [Authorize(Roles = "Админ, Читатель")]
    public class BooksController : Controller
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10, string name = "")
            => View(_bookService.BuildViewModelList(pageNumber, pageSize, name).Result);

        public IActionResult Create()
        {
            ViewBag.ActionName = "Создание";
            ViewBag.MethodName = "Create";

            return View("CreateUpdate", _bookService.BuildForm());
        }

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

        public IActionResult Update(int id)
        {
            ViewBag.ActionName = "Редактирование";
            ViewBag.MethodName = "Update";

            return View("CreateUpdate", _bookService.BuildFormById(id));
        }

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

        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.Delete(id);

            return Content("OK");
        }
    }
}