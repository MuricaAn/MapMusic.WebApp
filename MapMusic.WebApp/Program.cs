using MapMusic.BusinessLogic.Implementation.Organizer.Mappings;
using MapMusic.DataAccess;
using MapMusic.Entities;
using MapMusic.WebApp;
using MapMusic.WebApp.Code;
using MapMusic.WebApp.Code.ExtensionMethods;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddDbContext<MapMusicContext>(o =>
{
    o.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
});
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCurrentUser();
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(ToOrganizerRequestShow)));
builder.Services.AddPresentation();
builder.Services.AddBusinessLogic();
builder.Services.AddPolicy();
// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(GlobalExceptionFilterAttribute));
});



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Account/Login*";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });
var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
