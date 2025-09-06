using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using TaskManagerApp1.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔧 Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// ✅ WeatherForecastService може залишатися Singleton
builder.Services.AddSingleton<WeatherForecastService>();

// ✅ Scoped — правильно для сервісу, який використовує DbContext
builder.Services.AddScoped<TaskService>();

// ✅ Реєстрація AppDbContext з SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));


var app = builder.Build();

// 🔧 Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
