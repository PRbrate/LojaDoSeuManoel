using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaDoSeuManoel.Domain;

namespace LojaDoSeuManoel.Application
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(string id);
        Task<User> GetFindByEmailAsync(string email);
        Task<bool> Update(UserDtq userDTQ);
    }
}
