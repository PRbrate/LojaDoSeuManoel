using LojaDoSeuManoel.Domain;

namespace LojaDoSeuManoel.Application
{
    public interface IJwtService
    {
        Task<UserToken> GenerateJwt(User user);
    }
}
