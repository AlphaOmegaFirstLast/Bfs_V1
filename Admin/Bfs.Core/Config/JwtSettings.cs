namespace Bfs.Core.Config;

public class JwtSettings
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int AccessTokenExpireInMin { get; set; }
    public int RefreshTokenExpireInDay { get; set; }
}