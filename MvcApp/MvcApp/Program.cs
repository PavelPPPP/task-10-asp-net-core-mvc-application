using Microsoft.EntityFrameworkCore;
using Core.DataSource;
using MvcApp;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();
Log.Information("Starting application!");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((contesxt, configuration) => configuration.ReadFrom.Configuration(contesxt.Configuration));

var addedCustomServices = new ModuleAddCustomServices(builder.Services);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SchoolContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllersWithViews();
addedCustomServices.Load();


var app = builder.Build();
app.UseStaticFiles();
app.UseSerilogRequestLogging();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Course}/{action=Index}/{id?}");

app.Run();
