var builder = WebApplication.CreateBuilder(args);

// Register services here
builder.Services.AddControllersWithViews();
var app = builder.Build();

// Register middleware here
app.UseStatusCodePages();
app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();
