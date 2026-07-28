namespace Bfs.Core.Config;

public class BfsSettings
{
    public bool IsSecurityEnabled { get; set; } = true;
    public bool IsMigrationEnabled { get; set; } = false;   

    public bool IsMasterSystem { get; set; } = false;
    public bool IsIdentityWeb { get; set; } = false;
    public JwtSettings JwtSettings { get; set; } = new();
    public string AllowedOrigins { get; set; } = string.Empty;
    public DbConnections DbConnections { get; set; } = new();
    public ApiBaseUrls ApiBaseUrls { get; set; } = new();
}

public class DbConnections
{
    public string MasterConnection { get; set; } = string.Empty;
    public string MigrationConnection { get; set; } = string.Empty;
    public string TenantTestConnection { get; set; } = string.Empty;
}

public class ApiBaseUrls
{
    /// <summary>
    /// Base URL for the Auth API. It connects to The [Tenant Database].
    /// it is responsible for Auth user management within the tenant, such as creating users, managing user roles,
    /// and linking users to tenants.
    /// </summary>
    public string AuthApi { get; set; } = string.Empty;

    /// <summary>
    /// Base URL for the Identity Web API. It connects to [Master Database].
    /// Identity Web Api is responsible for AspNet user management, authentication, token management, 
    /// It is used by the Tenant Web API to manage user claims 
    /// and link users to tenants.
    /// </summary>
    public string IdentityWebApi { get; set; } = string.Empty;
}