using LojaDoSeuManoel.Core;

namespace LojaDoSeuManoel.Domain
{
    public class Pedidos : Entity
    {
        public string UserId { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
