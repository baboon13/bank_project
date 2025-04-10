using bank_project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace bank_project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserData> _userManager;
        private readonly SignInManager<UserData> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(SignInManager<UserData> signInManager, UserManager<UserData> userManager, ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    // 驗證帳號和密碼
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User logged in.");
                        // 登入成功後，導向使用者喜好清單頁面
                        return RedirectToAction("Index", "LikeList");
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToAction("LoginWith2fa", new { returnUrl });
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning("Account locked out.");
                        return RedirectToAction("Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new UserData
            {
                UserName = model.UserName,
                Email = model.Email,
                Account = model.Account,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                Console.WriteLine("使用者建立成功！");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "LikeList");    // 註冊後導向喜好清單               
            }

            foreach (var error in result.Errors)
            {
                Console.WriteLine($"❌ {error.Description}");
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); // 登出後回首頁
        }
    }
}
