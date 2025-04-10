using bank_project.Models;
using bank_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace bank_project.Controllers
{
    [Authorize]
    public class LikeListController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserData> _userManager;

        public LikeListController(ApplicationDbContext context, UserManager<UserData> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // 顯示喜好清單
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }

            var likeLists = await _context.LikeLists
                .Where(l => l.Account == currentUser.Account)
                .Include(l => l.Product)  // 加入產品資料，方便顯示
                .ToListAsync();

            return View(likeLists);
        }

        // 顯示新增喜好商品的頁面
        public IActionResult Create()
        {
            return View();
        }

        // 處理新增喜好商品的表單
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LikeListData model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return Challenge();  // 確保使用者已登入
                }

                model.UserId = currentUser.Id;  // 設置使用者 ID
                model.Account = currentUser.Account;  // 設置使用者帳號

                // 新增商品到資料庫
                _context.LikeLists.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");  // 重新導向到「我的喜好清單」
            }

            return View(model);  // 如果表單資料不合法，返回表單頁面
        }

    }
}
