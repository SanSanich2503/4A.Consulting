using Microsoft.AspNetCore.Mvc;

namespace LibraryWithUsersWebApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}