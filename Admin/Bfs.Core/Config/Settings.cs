namespace Bfs.Core.Config;

public class BfsSettings
{
    public bool IsSecurityEnabled { get; set; } = true;
    public string AllowedOrigins { get; set; } = string.Empty;
    public DbConnections? DbConnections { get; set; }
    public JwtSettings JwtSettings { get; set; } = new();
    public ApiBaseUrls? ApiBaseUrls { get; set; }
}

public class DbConnections
{
    public string BestFitConnection { get; set; } = string.Empty;
    public string InfrastructureConnection { get; set; } = string.Empty;
    public string AuthConnection { get; set; } = string.Empty;
    public string StockExConnection { get; set; } = string.Empty;
    public string StoresConnection { get; set; } = string.Empty;
}

public class ApiBaseUrls
{
    public string AuthApi { get; set; } = string.Empty;
    public string StockExApi { get; set; } = string.Empty;
}