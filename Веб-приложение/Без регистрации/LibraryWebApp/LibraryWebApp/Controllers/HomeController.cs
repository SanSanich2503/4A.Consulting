using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}