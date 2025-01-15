using Application.Repositories;
using Application.Repositories.Interfaces;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationConfigExtensions
{
    public static void AddServicesAndRepositories(this IServiceCollection services)
    {
        services.AddRepositories();
        services.AddServices();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IMovieTheaterService, MovieTheaterService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IMovieService, MovieService>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMovieTheaterRepository, MovieTheaterRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
    }
}