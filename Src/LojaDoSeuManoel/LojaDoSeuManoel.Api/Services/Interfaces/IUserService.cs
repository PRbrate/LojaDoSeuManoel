using LojaDoSeuManoel.Api.Dtos;
using LojaDoSeuManoel.Api.Dtqs;
using LojaDoSeuManoel.Api.Entities;

namespace LojaDoSeuManoel.Api.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(string id);
        Task<bool> UpdateRole(string userId);
        Task<User> GetFindByEmailAsync(string email);
        Task<bool> Update(UserDtq userDTQ);
    }
}
