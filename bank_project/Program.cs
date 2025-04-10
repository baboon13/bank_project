using bank_project.Models;
using bank_project.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 資料庫上下文配置
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// 添加並配置 Identity 服務
builder.Services.AddDefaultIdentity<UserData>(options =>
{ 
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 3;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    // 使用 Email 作為用戶名
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// 配置登入後的行為
builder.Services.ConfigureApplicationCookie(options =>
{
    //options.LoginPath = "/Identity/Account/Login";
    //options.LogoutPath = "/Identity/Account/Logout";
    //options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.LoginPath = "/Account/Login"; // 確保登錄頁的路徑
    options.LogoutPath = "/Account/Logout"; // 登出頁
    options.AccessDeniedPath = "/Account/AccessDenied"; // 訪問被拒頁
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;

    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.Redirect("/Identity/Account/Login");
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.Redirect("/Identity/Account/AccessDenied");
        return Task.CompletedTask;
    };

});

builder.Services.AddRazorPages();

var app = builder.Build();

// 配置HTTP請求管道
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 注意順序：先認證再授權
app.UseAuthentication();
app.UseAuthorization();

// 映射路由
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();