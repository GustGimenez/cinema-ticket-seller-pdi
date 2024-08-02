using cinema_ticket_seller_pdi.Models;

namespace cinema_ticket_seller_pdi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;

        public AuthenticationService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public string? AuthenticateUser(User user, string password)
        {
            var hasCorrectPassword = CheckPassword(user, password);
            if (!hasCorrectPassword)
            {
                return null;
            }

            return _tokenService.GenerateJWTForUser(user);;
        }

        private static bool CheckPassword(User user, string password)
        {
            // Encrypt password and compare
            return user.Password == password;
        }
    }
}
