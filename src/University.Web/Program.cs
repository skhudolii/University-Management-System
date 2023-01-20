using Microsoft.EntityFrameworkCore;
using University.Core.Repositories;
using University.Infrastructure.Data;
using University.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// REGISTER SERVICES HERE
builder.Services.AddControllersWithViews();

// use database
builder.Services.AddDbContext<UniversityDbContext>(opts =>
{
    opts.UseSqlServer(
        builder.Configuration["ConnectionStrings:UniversityConnection"]);
});

// add Infrastructure Layer
builder.Services.AddScoped<IAcademicEmployeesRepository, AcademicEmployeesRepository>();

var app = builder.Build();

// REGISTER MIDDLEWARE HERE
app.UseStatusCodePages();
app.UseStaticFiles();
app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();
