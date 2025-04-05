using bank_project.Models;
using bank_project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bank_project.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext context;

        public ProductController(ApplicationDbContext context)
        {
            this.context = context;
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
    }
}
