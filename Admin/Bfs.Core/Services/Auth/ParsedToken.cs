namespace Bfs.Core.Services.Auth;

public class TokenParsed
{
    public string UserId { get; set; } = string.Empty;
    public string Exp { get; set; } = string.Empty;
    public List<string> Role { get; set; } = new();
    public List<string> App { get; set; } = new();
    public List<string> Api { get; set; } = new();
    public List<string> Method { get; set; } = new();
}