using cinema_ticket_seller_pdi.Exceptions;
using cinema_ticket_seller_pdi.Repositories;
using cinema_ticket_seller_pdi.Schemas;
using cinema_ticket_seller_pdi.Services;
using Microsoft.AspNetCore.Mvc;

namespace cinema_ticket_seller_pdi.Controllers
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
                throw new NotFoundException("Usuário não encontrado");
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