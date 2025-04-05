using bank_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bank_project.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext context;

        /*public UserController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var user = context.Users.ToList();
            return View(user);
        }*/
    }
}
