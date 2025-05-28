using LojaDoSeuManoel.Core;

namespace LojaDoSeuManoel.Domain
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Length { get; set; }
    }
}
