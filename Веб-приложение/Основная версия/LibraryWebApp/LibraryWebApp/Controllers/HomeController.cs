using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers;

/// <summary>
/// Контроллер для работы с главной страницей приложения
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    /// Отображение главной страницы приложения
    /// </summary>
    /// <returns></returns>
    public IActionResult Index() => View();
}