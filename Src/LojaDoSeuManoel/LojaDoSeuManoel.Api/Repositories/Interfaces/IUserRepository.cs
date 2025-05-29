using LojaDoSeuManoel.Api.Entities;

namespace LojaDoSeuManoel.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetList();
        Task<User> GetUserFromId(string id);
        Task<User> GetFindByEmailAsync(string email);
        Task<bool> Update(User user);
        void Dispose();
    }
}
