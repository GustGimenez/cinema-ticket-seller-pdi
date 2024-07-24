using cinema_ticket_seller_pdi.Repositories;
using cinema_ticket_seller_pdi.Schemas;
using cinema_ticket_seller_pdi.Services;
using Microsoft.AspNetCore.Mvc;

namespace cinema_ticket_seller_pdi.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService authenticationService, IUserRepository userRepository) : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;
        private readonly IUserRepository _userRepository = userRepository;

        [HttpPost]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserSchema authenticationSchema)
        {
            var user = await _userRepository.FindByDocument(authenticationSchema.Document);

            if (user == null)
            {
                return NotFound();
            }

            var token = _authenticationService.AuthenticateUser(user, user.Password);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }
    }
}
