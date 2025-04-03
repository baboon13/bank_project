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
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index");
    }
}