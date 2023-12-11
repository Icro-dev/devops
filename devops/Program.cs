using devops;
using devops.Areas.Identity;
using devops.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using devops.Controllers;

var builder = WebApplication.CreateBuilder(args);


// load .env file
var root = Directory.GetCurrentDirectory();
Debug.Print(root);
var dotenvPath = Path.Combine(root, ".env");
var dotenv = new DotEnv(dotenvPath);
string Datasource = dotenv.Get("hostname");
string User_Id = dotenv.Get("username");
string Password = dotenv.Get("password");
string Database = dotenv.Get("database");

var connectionString = "Data Source=" + Datasource + ";" +
                       "User ID=" + User_Id + ";" +
                       "Password=" + Password + ";" +
                       "Database=" + Database + ";" +
                       "TrustServerCertificate=true";


// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddTransient<ArticlesController>();

var app = builder.Build();


/*var connectionString = "Data Source=" + env["hostname"] + ";" +
                       "User ID=" + env["USERNAME"] + ";" +
                       "Password=" + env["password"] + ";" +
                       "Database=" + env["database"] + ";" +
                       "TrustServerCertificate=true";*/

var config =
    new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .Build();

var env = Environment.GetEnvironmentVariables();


//Migration and seeding data
using (var scope = app.Services.CreateScope())
{
    //running migrations at startup
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
    //adding seeddata

    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
