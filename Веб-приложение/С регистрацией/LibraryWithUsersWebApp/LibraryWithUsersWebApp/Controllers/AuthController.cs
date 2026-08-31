using Data.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace InternetShopApp.Controllers;

/// <summary>
/// Контроллер для работы с аутентификацией и авторизацией пользователей
/// </summary>
public class AuthController : Controller
{
    private readonly AuthService _authService; // Сервис для работы с аутентификацией и авторизацией пользователей
    
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Получение формы для входа в систему
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Login() => View(new LoginForm());

    /// <summary>
    /// Вход в систему
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginForm model) 
        => _authService.Login(model, ModelState).Result.Item1 ? RedirectToAction("Index", "Home") : View(model);
    
    /// <summary>
    /// Получение формы для регистрации пользователя
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Register() => View(new RegisterForm());

    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterForm model) 
        => _authService.Register(model, ModelState).Result.Item1? RedirectToAction("Index", "Home") : View(model);
    
    /// <summary>
    /// Выход из системы
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Logout() => _authService.Logout().Result.Item1
        ? RedirectToAction("Login", "Auth")
        : Content("Произошла внутрення ошибка сервера");

    /// <summary>
    /// Получение сообщения о запрещённом доступе в систему
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult AccessDenied() => Content("Доступ запрещен");
}