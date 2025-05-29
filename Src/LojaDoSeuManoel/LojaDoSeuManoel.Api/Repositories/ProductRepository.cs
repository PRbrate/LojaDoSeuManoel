using LojaDoSeuManoel.Api.Entities;
using LojaDoSeuManoel.Api.Repositories.Context;
using LojaDoSeuManoel.Api.Repositories.Interfaces;
using LojaDoSeuManoel.Core.BarberShop.Core;
using Microsoft.EntityFrameworkCore;

namespace LojaDoSeuManoel.Api.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly LojaDoSeuManoelContext _context;
        public ProductRepository(LojaDoSeuManoelContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProducts()
        {
            var items = await _context.Products.ToListAsync();

            return items;
        }
    }
}
