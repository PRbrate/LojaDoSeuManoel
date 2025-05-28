using LojaDoSeuManoel.Api.Dtos;
using LojaDoSeuManoel.Api.Entities;

namespace LojaDoSeuManoel.MappingsConfig
{
    public static class AutoMapperUser
    {
        public static User Map(this RegisterUserDto userDto) => new User
        {
            Name = userDto.Name,
            Email = userDto.Email,
            UserName = userDto.Email.Split('@')[0],
            Address = userDto.Address,
            PasswordHash = userDto.Password
        };
    }
}
