using Microsoft.EntityFrameworkCore;
using EmployeeArrivalApp.Data;
using EmployeeArrivalApp.DataAccess.Contracts;
using EmployeeArrivalApp.DataAccess.Services;
using EmployeeArrivalApp.Hubs;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyContext>(options =>
options.UseSqlServer(connectionString));
builder.Services.AddSingleton<ISubscriptionService, SubscribtionService>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IArrivalService, ArrivalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapHub<EmployeeArrivalHub>("/employeeArrivalHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
