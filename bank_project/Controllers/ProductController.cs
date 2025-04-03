using bank_project.Services;
using Microsoft.AspNetCore.Mvc;

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
    }
}
