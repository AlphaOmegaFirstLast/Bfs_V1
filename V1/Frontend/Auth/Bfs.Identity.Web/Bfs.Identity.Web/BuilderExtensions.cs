//using Bfs.Auth.Client;
using Bfs.Core.Config;
using Bfs.Core.Services.Auth;
using Bfs.Identity.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Bfs.Identity.Web;
public static class BuilderExtensions
{
    public static void RegisterDbContext(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        if (settings != null && settings.DbConnections != null)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(settings.DbConnections.MasterConnection));
        }
    }

    public static void RegisterIdentity<TContext>(this WebApplicationBuilder builder) where TContext : DbContext
    {
        //ClaimsIdentity: Gets or sets the ClaimsIdentityOptions for the identity system.
        //Lockout:Gets or sets the LockoutOptions for the identity system.
        //Password:Gets or sets the PasswordOptions for the identity system.
        //SignIn:Gets or sets the SignInOptions for the identity system.
        //Stores:Gets or sets the StoreOptions for the identity system.
        //Tokens:Gets or sets the TokenOptions for the identity system.
        //User:Gets or sets the UserOptions for the identity system.

        // Example of configuring Identity options
        // Using AuthUser as the user type

        builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        {
            //options.ClaimsIdentity
            options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;

            //options.Lockout
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;

            //options.Password
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;

            //options.SignIn
            options.SignIn.RequireConfirmedAccount = true;
            options.SignIn.RequireConfirmedEmail = true;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            //options.Stores
            options.Stores.ProtectPersonalData = false;

            //options.Tokens
            options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
            options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;

            //options.User
            options.User.RequireUniqueEmail = true;
        }
        )
        .AddEntityFrameworkStores<TContext>();
        //.AddDefaultTokenProviders();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.SlidingExpiration = true;
            options.Cookie.SameSite = SameSiteMode.Strict;

            // other options
        });
    }

    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITokenService, TokenService>();
    }

    //public static void RegisterHttpClients(this WebApplicationBuilder builder, BfsSettings? settings)
    //{
    //    if (settings !=null && settings.ApiBaseUrls !=null)
    //    {
    //        var clientConfig = new Action<HttpClient>(client =>
    //        {
    //            client.BaseAddress = new Uri(settings.ApiBaseUrls.AuthApi); // Auth API base URL
    //            client.Timeout = TimeSpan.FromSeconds(30);
    //            client.DefaultRequestHeaders.Add("Accept", "application/json");
    //        });

    //        builder.Services.AddHttpClient<AuthClient>("AuthApi", clientConfig); // search httpClient wrapper
    //    }
    //}
}


