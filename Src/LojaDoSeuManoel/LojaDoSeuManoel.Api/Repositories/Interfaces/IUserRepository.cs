using LojaDoSeuManoel.Api.Entities;

namespace LojaDoSeuManoel.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(int id);
        Task<User> GetFindByEmailAsync(string email);
        Task<User> GetUserFromId(string id);
        Task<bool> Update(User user);
        void Dispose();
    }
}
