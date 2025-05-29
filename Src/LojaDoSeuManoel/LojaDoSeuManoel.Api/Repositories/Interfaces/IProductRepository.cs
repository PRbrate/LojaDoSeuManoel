using LojaDoSeuManoel.Api.Entities;
using LojaDoSeuManoel.Core;

namespace LojaDoSeuManoel.Api.Repositories.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<List<Product>> GetProducts();
    }
}
