using System.Text.Json.Serialization;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectWired.Core.Models;
using ProjectWired.Core.Repositories;
using ProjectWired.Core.Services;
using ProjectWired.Core.UnitOfWorks;
using ProjectWired.Data;
using ProjectWired.Data.Repositories;
using ProjectWired.Data.UnitOfWorks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire(x =>
    x.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(ProjectWired.Service.Services.Service<>));
builder.Services.AddScoped<IExamService, ProjectWired.Service.Services.ExamService>();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WiredDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Identity iþlemleri
builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<WiredDbContext>().AddDefaultTokenProviders();

// Cookie iþlemleri
CookieBuilder cookieBuilder = new CookieBuilder();

cookieBuilder.Name = "ProjectWired";
cookieBuilder.HttpOnly = false;
cookieBuilder.SameSite = SameSiteMode.Lax;
cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;

builder.Services.ConfigureApplicationCookie(opts =>
{
    opts.LoginPath = new PathString("/Home/Login");
    opts.LogoutPath = new PathString("/Member/Logout");
    opts.Cookie = cookieBuilder;
    opts.SlidingExpiration = true;
    opts.ExpireTimeSpan = System.TimeSpan.FromDays(60);
    opts.AccessDeniedPath = new PathString("/Member/AccessDenied");
});

var app = builder.Build();

app.UseHangfireDashboard();
BackgroundJob.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));

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
    pattern: "{controller=Home}/{action=Login}");

app.MapControllerRoute(
    name: "exam",
    pattern: "{controller=Home}/{action=Exam}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHangfireDashboard();
});
app.Run();
