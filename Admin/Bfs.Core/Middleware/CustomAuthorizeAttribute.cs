using Microsoft.AspNetCore.Authorization;

namespace Bfs.Core.Middleware;

public class CustomAuthorizeAttribute : AuthorizeAttribute
{
    public CustomAuthorizeAttribute(string requirement)
    {
        Policy = $"DynamicPolicy:{requirement}";
    }
}