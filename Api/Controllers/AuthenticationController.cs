using Application.Repositories.Interfaces;
using Application.Schemas;
using Application.Services.Interfaces;
using Commons.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRepository _userRepository;

        public AuthenticationController(IAuthenticationService authenticationService, IUserRepository userRepository)
        {
            _authenticationService = authenticationService;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserSchema authenticationSchema)
        {
            var user = await _userRepository.FindByDocument(authenticationSchema.Document);

            if (user == null)
            {
                throw new DataNotFoundException("Usuário não encontrado");
            }

            var token = _authenticationService.AuthenticateUser(user, user.Password);

            if (token == null)
            {
                throw new UnauthorizedException("Dados incorretos");
            }

            return Ok(new { token });
        }
    }
}