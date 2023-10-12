using Microsoft.EntityFrameworkCore;
using RecordCollection.DataAccess;
using Serilog;
using System;

var builder = WebApplication.CreateBuilder(args);

var log = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddSingleton<Serilog.ILogger>(log);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("RecordCollectionDb");

builder.Services.AddDbContext<RecordCollectionContext>(options => 
    options.UseNpgsql(connectionString)
    .UseSnakeCaseNamingConvention());

Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("Logs/Logs.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

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
