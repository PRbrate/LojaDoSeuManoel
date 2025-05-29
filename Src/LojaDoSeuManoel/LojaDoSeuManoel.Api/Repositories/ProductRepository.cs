using LojaDoSeuManoel.Api.Entities;
using LojaDoSeuManoel.Api.Repositories.Context;
using LojaDoSeuManoel.Api.Repositories.Interfaces;
using LojaDoSeuManoel.Core.BarberShop.Core;

namespace LojaDoSeuManoel.Api.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly LojaDoSeuManoelContext _context;
        public ProductRepository(LojaDoSeuManoelContext context) : base(context)
        {
            _context = context;
        }
    }
}
