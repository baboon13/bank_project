using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using bank_project.Services;
using System.Linq; 
using System.Threading.Tasks; 

public class HomeController : Controller
{
    private readonly IUserService _userService; // �Τ@�ܼƦW��

    public HomeController(IUserService userService) // �Τ@�ѼƦW��
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
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()), // �ץ� ClaimTypes
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

        return Json(new { success = false, message = "�ϥΪ̦W�٩αK�X���~" });
    }

    [HttpPost]
    public async Task<IActionResult> Register(Users user)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);
            return Json(new { success = false, message = string.Join(", ", errors) });
        }

        var confirmPassword = Request.Form["ConfirmPassword"];
        if (user.Password1 != confirmPassword)
        {
            return Json(new { success = false, message = "�K�X�P�T�{�K�X���@�P" });
        }

        if (await _userService.RegisterAsync(user))
        {
            return Json(new
            {
                success = true,
                message = "���U���\�A�еn�J",
                redirectUrl = Url.Action("Index", "Home")
            });
        }

        return Json(new { success = false, message = "�ϥΪ̦W�٤w�s�b" });
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index");
    }
}