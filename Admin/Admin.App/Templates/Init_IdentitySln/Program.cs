using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Bfs.Core.Config;
using Bfs.Core.Services.Auth;
using Bfs.Auth.Client;
using [TemplateSln].Web;
using [TemplateSln].Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var settings = builder.Configuration.GetSection("Settings").Get<BfsSettings>();
builder.Services.Configure<BfsSettings>(builder.Configuration.GetSection("Settings"));

// Add Aspire support for development. Bfs.Core.AspireExtensions
builder.AddServiceDefaults(); builder.RegisterDbContext(settings);
builder.RegisterIdentity<ApplicationDbContext>();
builder.RegisterServices();
builder.RegisterHttpClients(settings);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages();
builder.Services.AddControllers();

// Add HttpClient factory
builder.Services.AddHttpClient();

// Add cookie policy if needed
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
    options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
    options.Secure = CookieSecurePolicy.Always;
});

var app = builder.Build();

// Add Aspire support for development. Bfs.Core.AspireExtensions
app.MapDefaultEndpoints();

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

app.MapStaticAssets();

// Map Razor Pages and API Controllers
app.MapRazorPages()
   .WithStaticAssets();
app.MapControllers();

app.Run();

