using System.Net;
using System.Security.Claims;
using Library.Exceptions;

namespace Library.Services.Models;

public class CustomClaims
{
    public const string UserId = "UserId";
}

public static class ClaimsPrincipalExtensions
{
    public static long GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.Claims.FirstOrDefault(n => n.Type == CustomClaims.UserId);
        if (claim == null)
            throw new PortalException("Unauthorized", HttpStatusCode.Unauthorized);
        return long.Parse(claim.Value);
    }
}

public class CustomRoles
{
    public const string Admin = "ADMIN";
}