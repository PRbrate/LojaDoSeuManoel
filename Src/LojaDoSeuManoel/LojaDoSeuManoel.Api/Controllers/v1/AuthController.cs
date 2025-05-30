using LojaDoSeuManoel.Api.Dtos;
using LojaDoSeuManoel.Api.Entities;
using LojaDoSeuManoel.Api.Services;
using LojaDoSeuManoel.Core;
using LojaDoSeuManoel.Core.Base.Controller;
using LojaDoSeuManoel.MappingsConfig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LojaDoSeuManoel.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ApiControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        public readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, INotifier notifier, IUser user, IUserService userService, IJwtService jwtService) : base(notifier, user)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUser)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var user = registerUser.Map();

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                return Ok();
            }
            foreach (var error in result.Errors)
            {
                NotifyError(error.Description);
            }
            return CustomResponse();

        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUser)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var user = await _userService.GetFindByEmailAsync(loginUser.Email);

            if (user == null)
            {
                NotifyError("Login inválido.");
                return CustomResponse();
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginUser.Password, false, true);

            if (result.Succeeded)
                return CustomResponse(await _jwtService.GenerateJwt(user));

            if (result.IsLockedOut)
            {
                NotifyError("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse();
            }

            NotifyError("Usuário ou senha incorretos");
            return CustomResponse();
        }

    }
}

