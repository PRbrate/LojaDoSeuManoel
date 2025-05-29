using LojaDoSeuManoel.Api.Dtos;
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

        public static Product Map(this ProductDto productDto) => new Product
        {
            Id = productDto.Id,
            Name = productDto.Name,
            Length = productDto.Length,
            Height = productDto.Height,
            Width = productDto.Width
        };


        public static ProductDto Map(this Product product) => new(product.Id, product.Name, product.Width, product.Height, product.Length);

        public static List<ProductDto> Map(this ICollection<Product> products)
        {
            return products.Select(product => new ProductDto
            (
                product.Id,
                product.Name,
                product.Width,
                product.Height,
                product.Length
            )).ToList();
        }
    }
}
