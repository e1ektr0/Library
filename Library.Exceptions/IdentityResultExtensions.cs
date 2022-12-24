using System.Net;
using Microsoft.AspNetCore.Identity;

namespace Library.Exceptions;

public static class IdentityResultExtensions
{
    public static void ThrowException(this IdentityResult identityResult)
    {
        if (identityResult.Succeeded)
            return;

        var error = string.Join("; ", identityResult.Errors.Select(n => n.Description));
        throw new PortalException(error, HttpStatusCode.Forbidden);
    }
}