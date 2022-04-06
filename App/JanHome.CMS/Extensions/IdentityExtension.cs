using System;
using System.Linq;
using System.Security.Claims;
using MI.Dapper.Data.Constant;

namespace JanHome.CMS.Extensions
{
    public static class IdentityExtension
    {
        public static string GetSpecificClaim(this ClaimsIdentity claimsIdentity, string claimType)
        {
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == claimType);

            return (claim != null) ? claim.Value : string.Empty;
        }

        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = ((ClaimsIdentity) claimsPrincipal.Identity).Claims.Single(x =>
                x.Type == SystemConstants.UserClaim.Id);
            return Guid.Parse(claim.Value);
        }

        public static string GetRoleName(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = ((ClaimsIdentity) claimsPrincipal.Identity).Claims.Single(x =>
                x.Type == SystemConstants.UserClaim.Roles);
            return claim.Value;
        }
    }
}
