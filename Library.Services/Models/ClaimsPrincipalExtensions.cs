using System.Net;
using System.Security.Claims;
using Library.Exceptions;

namespace Library.Services.Models;

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