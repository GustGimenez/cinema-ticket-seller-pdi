using cinema_ticket_seller_pdi.Models;

namespace cinema_ticket_seller_pdi.Services
{
    public class AuthenticationService(ITokenService tokenService) : IAuthenticationService
    {
        private readonly ITokenService _tokenService = tokenService;

        public string? AuthenticateUser(User user, string password)
        {
            if (!HasCorrectPassword(user, password))
            {
                return null;
            }

            var token = _tokenService.GenerateJWTForUser(user);

            return token;
        }

        private bool HasCorrectPassword(User user, string password)
        {
            // Encrypt password and compare
            return user.Password == password;
        }
    }
}
