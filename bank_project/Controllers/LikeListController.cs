using bank_project.Models;
using bank_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

[Authorize]
public class LikeListController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<UserData> _userManager;  // 使用自定義 User 類別

    public LikeListController( ApplicationDbContext context, UserManager<UserData> userManager)  // 注入 UserManager<User>
     {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null)
        {
            // 當使用者未登入，導向自訂的登入頁面
            return RedirectToAction("Login", "Account"); // 指定導向的控制器和動作
        }

        var likeLists = await _context.LikeLists
            .Where(l => l.Account == currentUser.Account)  // 使用 Account 關聯
            .ToListAsync();

        return View(likeLists);
    }
}
