using bank_project.Common;
using bank_project.Repositories;
using bank_project.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//���U�ۭq�A��
//builder.Services.AddScoped<DapperContext>(); //DB�s�u

// ��Ʀs���h
//builder.Services.AddScoped<IProductRepository, ProductRepository>();

// �~���޿�h
//builder.Services.AddScoped<IProductService, ProductService>();
// �K�[�A�Ȩ�e��
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserService, UserService>();

// �K�[�Q�Ȱt�m (�Ω�K�X����)
builder.Configuration.AddInMemoryCollection(new[]
{
    new KeyValuePair<string, string>("Salt", "your-secret-salt-value")
});


// �K�[�A�Ȩ�e��
//builder.Services.AddControllersWithViews();

// �K�[�������ҪA��
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Index";
        options.AccessDeniedPath = "/Home/Index";
    });

var app = builder.Build();

// �t�mHTTP�ШD�޹D
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

// �K�[�������Ҥ�����
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
