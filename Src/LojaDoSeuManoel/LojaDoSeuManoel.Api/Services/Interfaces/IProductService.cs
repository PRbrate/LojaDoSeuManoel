using LojaDoSeuManoel.Api.Dtos;
using LojaDoSeuManoel.Api.Dtqs;

namespace LojaDoSeuManoel.Api.Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateProduct(ProductDtq productDtq);
        Task<bool> DeleteProduct(Guid productId);
        Task<List<ProductDto>> GetAllProducts();
        Task<ProductDto> GetProduct(Guid id);
        Task<bool> UpdateProduct(ProductDto haircutDtq);
    }
}
