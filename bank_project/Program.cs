using bank_project.Common;
using bank_project.Repositories;
using bank_project.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//註冊自訂服務
//builder.Services.AddScoped<DapperContext>(); //DB連線

// 資料存取層
//builder.Services.AddScoped<IProductRepository, ProductRepository>();

// 業務邏輯層
//builder.Services.AddScoped<IProductService, ProductService>();
// 添加服務到容器
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserService, UserService>();

// 添加鹽值配置 (用於密碼哈希)
builder.Configuration.AddInMemoryCollection(new[]
{
    new KeyValuePair<string, string>("Salt", "your-secret-salt-value")
});


// 添加服務到容器
//builder.Services.AddControllersWithViews();

// 添加身份驗證服務
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Index";
        options.AccessDeniedPath = "/Home/Index";
    });

var app = builder.Build();

// 配置HTTP請求管道
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 添加身份驗證中間件
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
