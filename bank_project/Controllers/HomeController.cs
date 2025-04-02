using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using bank_project.Services;
using System.Linq;
using System.Threading.Tasks;
using bank_project.Models;

public class HomeController : Controller
{
    private readonly IUserService _userService; // 統一變數名稱

    public HomeController(IUserService userService) // 統一參數名稱
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = await _userService.AuthenticateAsync(username, password);

        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()), // 修正 ClaimTypes
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Account", user.Account)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return Json(new
            {
                success = true,
                redirectUrl = Url.Action("Profile", "Account")
            });
        }

        return Json(new { success = false, message = "使用者名稱或密碼錯誤" });
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserData user)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);
            return Json(new { success = false, message = string.Join(", ", errors) });
        }

        var confirmPassword = Request.Form["ConfirmPassword"];
        if (user.password1 != confirmPassword)
        {
            return Json(new { success = false, message = "密碼與確認密碼不一致" });
        }

        if (await _userService.RegisterAsync(user))
        {
            return Json(new
            {
                success = true,
                message = "註冊成功，請登入",
                redirectUrl = Url.Action("Index", "Home")
            });
        }

        return Json(new { success = false, message = "使用者名稱已存在" });
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index");
    }
}