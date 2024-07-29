using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Data;
using MoneyTracker.Data.Models;
using MoneyTracker.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString,
    x =>
    {
        x.MigrationsHistoryTable(builder.Configuration["Database:MigrationsHistoryTable:Name"], builder.Configuration["Database:MigrationsHistoryTable:Schema"]);
    })
);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<User, Role>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;

})
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/signin";
    options.LogoutPath = "/account/signout";
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services
    .AddScoped<CurrencyService>()
    .AddScoped<EntryService>()
    .AddScoped<EntryTypeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles(new StaticFileOptions()
{
    RequestPath = "/money-tracker"
});
app.UsePathBase("/money-tracker");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
