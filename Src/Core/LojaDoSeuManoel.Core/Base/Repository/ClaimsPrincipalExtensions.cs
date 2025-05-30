using System.Security.Claims;

namespace LojaDoSeuManoel.Core
{
    public static class ClaimsPrincipalExtensions
    {
        public static long GetAccountId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var accountId = principal.Claims.FirstOrDefault().Value;
            return long.Parse(accountId);
        }
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal?.Claims.FirstOrDefault(c => c.Type == "UserId");
            return claim?.Value ?? string.Empty;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }
    }
}
