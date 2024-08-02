using cinema_ticket_seller_pdi.Repositories;
using cinema_ticket_seller_pdi.Services;

namespace cinema_ticket_seller_pdi.Configs
{
    public static class IoCConfig
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}