using LojaDoSeuManoel.Api.Entities;
using LojaDoSeuManoel.Api.Response;

namespace LojaDoSeuManoel.Api.Services
{
    public interface IJwtService
    {
        Task<UserToken> GenerateJwt(User user);
    }
}
