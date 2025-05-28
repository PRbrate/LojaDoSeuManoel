using LojaDoSeuManoel.Core;

namespace LojaDoSeuManoel.Api.Entities
{
    public class Requested : Entity
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
