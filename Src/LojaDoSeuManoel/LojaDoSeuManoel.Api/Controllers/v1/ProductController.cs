using LojaDoSeuManoel.Api.Dtos;
using LojaDoSeuManoel.Api.Dtqs;
using LojaDoSeuManoel.Api.Entities;
using LojaDoSeuManoel.Api.Services;
using LojaDoSeuManoel.Api.Services.Interfaces;
using LojaDoSeuManoel.Core;
using LojaDoSeuManoel.Core.Base.Controller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LojaDoSeuManoel.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : ApiControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(INotifier notifier, IUser user, IUserService userService, IProductService productService) : base(notifier, user)
        {
            _productService = productService;

        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductDtq productDtq)
        {
            var sucess = await _productService.CreateProduct(productDtq);

            return CustomResponse();
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult> GetProducts()
        {
            var sucess = await _productService.GetAllProducts();

            return CustomResponse(sucess);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {

            var sucess = await _productService.GetProduct(id);

            return CustomResponse(sucess);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {

            var sucess = await _productService.DeleteProduct(id);

            if (!sucess) NotifyError("Erro ao deletar o Produto");

            return CustomResponse(sucess);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductDtq productDtq)
        {

            var sucess = await _productService.UpdateProduct(id, productDtq);

            if (!sucess) NotifyError("Erro ao atualizar o produto");

            return CustomResponse(sucess);
        }
    }
}
