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
            if (productDtq.Width > 80 || productDtq.Length > 80 || productDtq.Height > 80)
            {
                Notifier("Nenhuma das dimenções pode ser maior que 80");
                return false;
            }
            var product = AutoMapperProduct.Map(productDtq);

            if (_boxService.VerifyBox(product).Count == 0)
            {
                Notifier("Não é possivel cadastrar esse produto, pois não caberá em nenhuma caixa");
                return false;
            }


            if (!ExecuteVatidation(new ProductValidation(), product)) return false;


            return await _productRepository.Create(product);


        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            var query = await _productRepository.GetById(productId);

            if (query == null)
            {
                Notifier("Não Foi encontrado Produto com o id passado");
                return false;
            }

            return await _productRepository.Delete(productId);
        }

        public async Task<List<ProductDto>> GetAllProducts()
        {
            var list = await _productRepository.GetProducts();

            return list.Map(); ;
        }

        public async Task<ProductDto> GetProduct(Guid id)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
            {
                Notifier("Não Foi encontrado Produto com o id passado");
                return null;
            }
            return AutoMapperProduct.Map(product);
        }

        public async Task<bool> UpdateProduct(Guid id, ProductDtq productDtq)
        {
            var response = await _productRepository.GetById(id);

            if (response == null)
            {
                Notifier("Não foi encontrado Produto com o Id informado para a atualização");
                return false;
            }

            var box = new BoxService();
            if (productDtq.Width > 80 || productDtq.Length > 80 || productDtq.Height > 80)
            {
                Notifier("Nenhuma das dimenções pode ser maior que 80");
                return false;
            }

            if (productDtq.Name != "string") response.Name = productDtq.Name;
            if (productDtq.Width != 0) response.Width = productDtq.Width;
            if (productDtq.Length != 0) response.Length = productDtq.Length;
            if (productDtq.Height != 0) response.Height = productDtq.Height;

            if (_boxService.VerifyBox(response).Count == 0)
            {
                Notifier("Não é possivel atualizar esse produto, pois não caberá em nenhuma caixa");
                return false;
            }


            if (!ExecuteVatidation(new ProductValidation(), response)) return false;


            return await _productRepository.Update(response);
        }
    }
}
