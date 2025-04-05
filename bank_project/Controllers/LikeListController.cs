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

    public LikeListController(
        ApplicationDbContext context,
        UserManager<UserData> userManager)  // 注入 UserManager<User>
    {
        _context = context;
        _userManager = userManager;
    }

    // 合併 UserLists 和 MyList 的功能
    public async Task<IActionResult> Index()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null)
        {
            return Challenge();
        }

        var likeLists = await _context.LikeLists
            .Where(l => l.Account == currentUser.Account)  // 使用 Account 關聯
            .ToListAsync();

        return View(likeLists);
    }
}
