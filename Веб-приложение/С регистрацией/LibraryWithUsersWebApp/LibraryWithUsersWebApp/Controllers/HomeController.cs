using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWithUsersWebApp.Controllers;

public class HomeController : Controller
{
    [Authorize(Roles = "Админ, Читатель")]
    public IActionResult Index() => View();
}