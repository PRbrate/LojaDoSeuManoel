using LojaDoSeuManoel.Api.Dtos;
using LojaDoSeuManoel.Api.Dtqs;
using LojaDoSeuManoel.Api.Entities;
using LojaDoSeuManoel.Api.Entities.Enums;

namespace LojaDoSeuManoel.MappingsConfig
{
    public static class AutoMapperUser
    {
        public static User Map(this RegisterUserDto userDto) => new User
        {
            Name = userDto.Name,
            Email = userDto.Email,
            Role = UserRole.Customer,
            UserName = userDto.Email.Split('@')[0],
            Address = userDto.Address,
            PasswordHash = userDto.Password
        };

        public static UserDto Map(this User user) => new
            (user.Id, user.Name, user.Email, user.Address, user.Role.ToString());

        public static List<UserDto> Map(this ICollection<User> users)
        {
            return users.Select(user => new UserDto
            (
                user.Id,
                user.Name,
                user.Email,
                user.Address,
                user.Role.ToString()


            )).ToList();
        }
    }
}
