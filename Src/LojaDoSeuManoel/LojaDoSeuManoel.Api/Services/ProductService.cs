using LojaDoSeuManoel.Api.Dtos;
using LojaDoSeuManoel.Api.Dtqs;
using LojaDoSeuManoel.Api.Entities.Validations;
using LojaDoSeuManoel.Api.MappingsConfig;
using LojaDoSeuManoel.Api.Repositories.Interfaces;
using LojaDoSeuManoel.Api.Services.Interfaces;
using LojaDoSeuManoel.Core;
using LojaDoSeuManoel.Core.Base.Service;

namespace LojaDoSeuManoel.Api.Services
{
    public class ProductService : ServiceBase, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly BoxService _boxService;

        public ProductService(IProductRepository productRepository, INotifier notifier, BoxService boxService) : base(notifier)
        {
            _productRepository = productRepository;
            _boxService = boxService;
        }

        public async Task<bool> CreateProduct(ProductDtq productDtq)
        {
            var box = new BoxService();   
            if(productDtq.Width > 80 || productDtq.Length > 80 || productDtq.Height > 80)
            {
                Notifier("Nenhuma das dimenções pode ser maior que 80");
                return false;
            }
            if(_boxService.VerifyBox(productDtq).Count == 0)
            {
                Notifier("Não é possivel cadastrar esse produto, pois não caberá em nenhuma caixa");
                return false;
            }

            var product = AutoMapperProduct.Map(productDtq);

            if (!ExecuteVatidation(new ProductValidation(), product)) return false;
      
            
            return await _productRepository.Create(product);


        }

        public Task<bool> DeleteProduct(Guid productId)
        {
            throw new NotImplementedException() 
        }

        public Task<List<ProductDto>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> GetHaircut(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProduct(ProductDto haircutDtq)
        {
            throw new NotImplementedException();
        }
    }
}
