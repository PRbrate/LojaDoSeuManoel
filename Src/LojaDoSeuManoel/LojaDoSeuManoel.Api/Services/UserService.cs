using LojaDoSeuManoel.Api.Dtos;
using LojaDoSeuManoel.Api.Dtqs;
using LojaDoSeuManoel.Api.Entities;
using LojaDoSeuManoel.Api.Repositories;
using LojaDoSeuManoel.MappingsConfig;

namespace LojaDoSeuManoel.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }


        public async Task<User> GetFindByEmailAsync(string email)
        {
            return await _userRepository.GetFindByEmailAsync(email);
        }
        public async Task<UserDto> GetUserById(string id)
        {
            var user = await _userRepository.GetUserFromId(id);

            return user.Map();
        }
        public async Task<bool> Update(UserDtq userDTQ)
        {
            var user = await _userRepository.GetUserFromId(userDTQ.Id);
            if (user == null) return false;

            user.Name = userDTQ.Name;
            user.Address = userDTQ.Address;

            return await _userRepository.Update(user);
        }
    }
}
