using Microsoft.EntityFrameworkCore;
using University.Core.Repositories;
using University.Core.Services;
using University.Core.Services.Interfaces;
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

// Add Core Layer
builder.Services.AddScoped<ISubjectsService, SubjectsService>();
builder.Services.AddScoped<ILectureRoomsService, LectureRoomsService>();

// Add Infrastructure Layer
builder.Services.AddScoped<IAcademicEmployeesRepository, AcademicEmployeesRepository>();
builder.Services.AddScoped<IFacultiesRepository, FacultiesRepository>();
builder.Services.AddScoped<IGroupsRepository, GroupsRepository>();
builder.Services.AddScoped<ILectureRoomsRepository, LectureRoomsRepository>();
builder.Services.AddScoped<ILecturesRepository, LecturesRepository>();
builder.Services.AddScoped<IStudentsRepository, StudentsRepository>();
builder.Services.AddScoped<ISubjectsRepository, SubjectsRepository>();

var app = builder.Build();

// REGISTER MIDDLEWARE HERE
app.UseStatusCodePages();
app.UseStaticFiles();
app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();
