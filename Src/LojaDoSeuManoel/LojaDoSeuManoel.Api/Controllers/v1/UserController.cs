using LojaDoSeuManoel.Api.Services;
using LojaDoSeuManoel.Core;
using LojaDoSeuManoel.Core.Base.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaDoSeuManoel.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;
        public UserController(INotifier notifier, IUser appUser, IUserService userService) : base(notifier, appUser)
        {
            _userService = userService;
        }


        [HttpGet("GetUser")]

        public async Task<IActionResult> GetUser(string userId)
        {

            var result = await _userService.GetUserById(userId);

            return CustomResponse(result);

        }

        [HttpGet("GetUsers")]

        public async Task<IActionResult> GetUsers()
        {

            var result = await _userService.GetUsers();

            return CustomResponse(result);

        }



        [HttpPut("UpdateRole")]

        public async Task<IActionResult> UpdateUserRole(string userId)
        {

            var result = await _userService.UpdateRole(userId);

            if (result)
            {
                return Ok();
            }
            else
            {
                NotifyError("Não foi possivel atualizar usuário");
            }
            return CustomResponse();

        }
    }
}
