using System.Security.Claims;

namespace E.API.Extension;

public static class HttpContextExtensions
{
    public static Guid GetIdentityIdClaimValue(this HttpContext context)
    {
        return GetGuidClaimValue("IdentityId", context);
    }

    private static Guid GetGuidClaimValue(string key, HttpContext context)
    {
        var identity = context.User.Identity as ClaimsIdentity;
        var claim = identity?.FindFirst(key)?.Value;

        if (string.IsNullOrEmpty(claim))
        {
            throw new Exception($"Claim '{key}' not found");
        }

        return Guid.Parse(claim);
    }
}
