using LojaDoSeuManoel.Api.Dtos;

namespace LojaDoSeuManoel.Api.Response
{
    public class UserToken
    {
        public string AccessToken { get; set; }
        public UserDto User { get; set; }
    }
}
