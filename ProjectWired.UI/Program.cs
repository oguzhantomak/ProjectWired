using Microsoft.EntityFrameworkCore;
using ProjectWired.Core.UnitOfWorks;
using ProjectWired.Data;
using ProjectWired.Data.UnitOfWorks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<WiredDbContext>(options =>
//{
//    options.UseNpgsql(builder.Configuration["ConnectionStrings:SqlConStr"].ToString(), o =>
//    {
//        o.MigrationsAssembly("ProjectWired.Data");
//    });
//});

string connString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(connString);

builder.Services.AddDbContext<WiredDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
