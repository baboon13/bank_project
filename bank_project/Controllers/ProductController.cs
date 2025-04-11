using bank_project.Models;
using bank_project.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace bank_project.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<UserData> _userManager;

        public ProductController(ApplicationDbContext context, UserManager<UserData> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var product = context.Products.ToList();
            return View(product);
        }
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductData product)
        {
            if (ModelState.IsValid)
            {
                this.context.Add(product);
                await this.context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await this.context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ProductData product)
        {
            if (id != product.No)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.context.Update(product);
                    await this. context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.No))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        private bool ProductExists(string id)
        {
            return  this.context.Products.Any(e => e.No == id);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await this.context.Products
                .FirstOrDefaultAsync(m => m.No == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var product = await this.context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            try
            {
                this.context.Products.Remove(product);
                await this.context.SaveChangesAsync();

                TempData["SuccessMessage"] = "產品已成功刪除";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                //_logger.LogError(ex, "刪除產品時發生錯誤");
                ModelState.AddModelError("", "無法刪除產品，可能已有關聯資料存在");
                return View(product);
            }
        }
        // 在 ProductController 中新增 AddToLikeList 動作
        public async Task<IActionResult> AddToLikeList(string productNo)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");  // 確保使用者已經登入
            }

            var product = await this.context.Products.FindAsync(productNo);
            if (product == null)
            {
                return NotFound();
            }

            var likeList = new LikeListData
            {
                Account = currentUser.Account,
                No = productNo,  // 設定商品編號
                Product = product,  // 關聯商品
                UserId = currentUser.Id,  // 設定 UserId 為當前使用者的 Id
                //OrderName = "Default Order"  // 設定 OrderName 的值
            };

            this.context.LikeLists.Add(likeList);
            await this.context.SaveChangesAsync();

            TempData["LikeSuccess"] = "已成功加入喜好清單！";
            return RedirectToAction("Index");
        }


    }
}
