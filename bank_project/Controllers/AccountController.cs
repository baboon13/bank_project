using bank_project.Common;
using bank_project.Models; // 確保這行存在
using bank_project.Models.ViewModels; // 添加這行
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace bank_project.Controllers
{
    [Authorize] // 需要登入才能訪問
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Profile()
        {
            // 取得當前登入用戶ID
            var userId = int.Parse(Users.FindFirstValue(ClaimTypes.NameIdentifier));
            Console.WriteLine($"當前用戶ID: {userId}");

            // 查詢用戶資料和喜好清單
            var users = await _context.Users.FindAsync(userId);
            if (users == null)
            {
                Console.WriteLine("未找到用戶");
                return NotFound();
            }
            Console.WriteLine($"用户账户: {users.Account}");

            var likeList = await _context.LikeList
                .Where(l => l.Account == users.Account)
                .ToListAsync();

            Console.WriteLine($"找到 {likeList.Count} 条喜好记录");

            var ViewModels = new UserProfileViewModel
            {
                Users = users,
                LikeList = likeList
            };

            return View(ViewModels);
        }
    }
   
}
