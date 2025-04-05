using bank_project.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ��Ʈw�W�U��t�m
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// �K�[�ðt�m Identity �A��
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 3;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

    // �ϥ� Email �@���Τ�W
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// �t�m�n�J�᪺�欰
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";

    // �n�J�᭫�w�V��ߦn�M��
    options.Events.OnSignedIn = async context =>
    {
        var userId = context.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
        context.Response.Redirect($"/LikeList/UserLists");
    };
});

builder.Services.AddRazorPages();

var app = builder.Build();

// �t�mHTTP�ШD�޹D
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// �`�N���ǡG���{�ҦA���v
app.UseAuthentication();
app.UseAuthorization();

// �M�g����
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();