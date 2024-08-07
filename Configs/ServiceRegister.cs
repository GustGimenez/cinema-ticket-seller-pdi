using System.Text;
using cinema_ticket_seller_pdi.Repositories;
using cinema_ticket_seller_pdi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace cinema_ticket_seller_pdi.Configs
{
    public static class ServiceRegister
    {
        public static void AddServicesAuthentication(IServiceCollection services, string key)
        {
            services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IMovieTheaterRepository, MovieTheaterRepository>();
            services.AddScoped<IMovieTheaterService, MovieTheaterService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
        }
    }
}