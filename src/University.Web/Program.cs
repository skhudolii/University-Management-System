using Microsoft.EntityFrameworkCore;
using University.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Register services here
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<UniversityDbContext>(opts =>
{
    opts.UseSqlServer(
        builder.Configuration["ConnectionStrings:UniversityConnection"]);
});

var app = builder.Build();

// Register middleware here
app.UseStatusCodePages();
app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();
