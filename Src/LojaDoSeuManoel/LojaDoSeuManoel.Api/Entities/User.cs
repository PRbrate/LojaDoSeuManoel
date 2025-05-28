using LojaDoSeuManoel.Api.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace LojaDoSeuManoel.Api.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
        }
        public string Name { get; set; }
        public string Address { get; set; }
        public UserRole Role { get; set; }
        public ICollection<Requested> Requesteds { get; set; }
    }
}
