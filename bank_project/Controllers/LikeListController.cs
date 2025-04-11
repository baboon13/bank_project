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
                // 當使用者未登入，導向自訂的登入頁面
                return RedirectToAction("Login", "Account"); // 指定導向的控制器和動作
            }

            var likeLists = await _context.LikeLists
                .Where(l => l.Account == currentUser.Account)  // 使用 Account 關聯
                .Include(l => l.Product)  // 加這行才會載入導航屬性
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

        // 顯示刪除確認頁面 (GET)
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.LikeLists
                .Include(l => l.Product)
                .FirstOrDefaultAsync(m => m.SN == id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item); // 要對應到你貼的 View
        }

        // 確認刪除 (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLikeList(int SN)
        {
            var item = await _context.LikeLists.FindAsync(SN);
            if (item == null)
            {
                return NotFound();
            }

            _context.LikeLists.Remove(item);
            await _context.SaveChangesAsync();

            TempData["DeleteSuccess"] = "已成功刪除喜好項目";
            return RedirectToAction(nameof(Index));
        }


    }
}
