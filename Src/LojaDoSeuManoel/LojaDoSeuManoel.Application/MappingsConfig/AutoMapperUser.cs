using LojaDoSeuManoel.Domain;

namespace LojaDoSeuManoel.Application
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
