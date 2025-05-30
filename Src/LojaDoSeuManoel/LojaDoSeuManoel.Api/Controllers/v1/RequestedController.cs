using LojaDoSeuManoel.Api.Dtos;
using LojaDoSeuManoel.Api.Entities;
using LojaDoSeuManoel.Api.Services.Interfaces;
using LojaDoSeuManoel.Core;
using LojaDoSeuManoel.Core.Base.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaDoSeuManoel.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RequestedController : ApiControllerBase
    {
        private readonly IRequestedService _requestedService;

        public RequestedController(INotifier notifier, IUser appUser, IRequestedService requestedService) : base(notifier, appUser)
        {
            _requestedService = requestedService;
        }


        [HttpPost]
        public async Task<ActionResult> PostRequested(List<RequestedDto> requesteds)
        {
    

           var response =  await _requestedService.CreateRequested(requesteds, UserId);

            if (response == null)
            {
                return CustomResponse("Não foi possivel salvar o Pedido");
            }
            return CustomResponse(response);
        }

    }
}
