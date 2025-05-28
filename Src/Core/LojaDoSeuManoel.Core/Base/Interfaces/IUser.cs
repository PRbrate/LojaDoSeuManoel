using System.Security.Claims;

namespace LojaDoSeuManoel.Core
{
    public interface IUser
    {
        string Name { get; }
        string GetUserId();
        long GetAccountId();
        string GetUserEmail();
        bool IsAuthenticated();
        string GetLocalIpAddress();
        string GetRemoteIpAddress();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
