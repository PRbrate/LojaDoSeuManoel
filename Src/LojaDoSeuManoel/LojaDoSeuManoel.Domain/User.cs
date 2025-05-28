using Microsoft.AspNetCore.Identity;

namespace LojaDoSeuManoel.Domain
{
    public class User : IdentityUser
    {
        public User()
        {
        }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
