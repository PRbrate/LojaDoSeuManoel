using LojaDoSeuManoel.Api.Dtqs;
using LojaDoSeuManoel.Api.Entities;
using LojaDoSeuManoel.Api.Services;
using LojaDoSeuManoel.Core;
using LojaDoSeuManoel.Core.Base.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaDoSeuManoel.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ValuesController : ApiControllerBase
    {
        private readonly BoxService _boxService;

        public ValuesController(BoxService boxService, INotifier notifier, IUser user) : base(notifier, user) 
        {
            _boxService = boxService;
        }

        [AllowAnonymous]
        [HttpPost("verificar")]
        public ActionResult<List<string>> VerificarProduto(ProductDtq produto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
    
            //if (produto.Length != 3)
            //    return BadRequest("Você deve fornecer exatamente 3 dimensões decimais.");

            var resultado = _boxService.VerifyBox(produto);
            return Ok(resultado);
        }

    }
}
