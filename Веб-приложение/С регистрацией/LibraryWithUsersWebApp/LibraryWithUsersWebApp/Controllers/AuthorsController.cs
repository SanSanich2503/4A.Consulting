using Data.ViewModels.Authors;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace LibraryWebApp.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AuthorService _authorService;

        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10, string name = "")
            => View(_authorService.BuildViewModelList(pageNumber, pageSize, name).Result);

        public IActionResult Create()
        {
            ViewBag.ActionName = "Создание";
            ViewBag.MethodName = "Create";

            return View("CreateUpdate", _authorService.BuildForm());
        }

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

        public IActionResult Update(int id)
        {
            ViewBag.ActionName = "Редактирование";
            ViewBag.MethodName = "Update";

            return View("CreateUpdate", _authorService.BuildFormById(id));
        }

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

        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.Delete(id);

            return Content("OK");
        }
    }
}