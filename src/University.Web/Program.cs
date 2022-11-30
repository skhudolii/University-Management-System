using Microsoft.EntityFrameworkCore;
using University.Core.Repositories;
using University.Infrastructure.Data;
using University.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Register services here
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<UniversityDbContext>(opts =>
{
    opts.UseSqlServer(
        builder.Configuration["ConnectionStrings:UniversityConnection"]);
});
builder.Services.AddScoped<IFacultyRepository, EFFacultyRepository>();

var app = builder.Build();

// Register middleware here
app.UseStatusCodePages();
app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();
