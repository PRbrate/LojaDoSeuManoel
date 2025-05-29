using LojaDoSeuManoel.Api.Dtqs;
using LojaDoSeuManoel.Api.Entities;

namespace LojaDoSeuManoel.Api.MappingsConfig
{
    public static class AutoMapperProduct
    {
        public static Product Map(this ProductDtq productDtq) => new Product
        {
            Id = new Guid(),
            Name = productDtq.Name,
            Length = productDtq.Length,
            Height = productDtq.Height,
            Width = productDtq.Width
        };
    }
}
