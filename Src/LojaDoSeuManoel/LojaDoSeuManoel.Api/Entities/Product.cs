using LojaDoSeuManoel.Core;

namespace LojaDoSeuManoel.Api.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Length { get; set; }
        public Guid RequestedId { get; set; }
        public Requested Requested { get; set; }
    }
}
