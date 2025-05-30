using Azure.Core;
using LojaDoSeuManoel.Api.Dtos;
using LojaDoSeuManoel.Api.Entities;
using LojaDoSeuManoel.Api.MappingsConfig;
using LojaDoSeuManoel.Api.Repositories.Interfaces;
using LojaDoSeuManoel.Api.Services.Interfaces;
using LojaDoSeuManoel.Core;
using LojaDoSeuManoel.Core.Base.Service;

namespace LojaDoSeuManoel.Api.Services
{
    public class RequestedService : ServiceBase, IRequestedService
    {
        private readonly IProductRepository _productRepository;

        private readonly BoxService _boxService;

        public RequestedService(INotifier notifier, IProductRepository productRepository, BoxService boxService) : base(notifier)
        {
            _productRepository = productRepository;
            _boxService = boxService;
        }

        public async Task<List<RequestedResponseDto>> CreateRequested(ICollection<RequestedDto> requestedDto, string userId)
        {
            var response = new List<RequestedResponseDto>();

            foreach (var requestedDtoItem in requestedDto)
            {

                var requested = await AddProductRequested(requestedDtoItem.Products);
                
                if (requested == null) 
                {
                    Notifier("Um ou mais produtos não foram encontrados");
                    return response;
                }
                requested.UserId = userId;

                var packingResult = _boxService.PackProducts(requested.Products.ToList());

                response.Add(new RequestedResponseDto
                {
                    Id = requested.Id,
                    UserId = userId,
                    Products = AutoMapperProduct.Map(requested.Products),
                    NumBox = packingResult.Count,
                    BoxNames = packingResult.Boxes.Select(b => b.BoxName).ToList(),
                });
                

            }

            return response;

        }

        private async Task<Requested> AddProductRequested(ICollection<Guid> productsId)
        {
            var requested = new Requested();

            foreach (Guid guid in productsId)
            {
                var product = await _productRepository.GetById(guid);
                if (product == null)
                {
                    Notifier($"Produto com id {guid} não foi encontrado");
                    return null;
                }

                requested.Products.Add(product);
            }


            return requested;
        }
    }
}
