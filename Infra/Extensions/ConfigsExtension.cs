using System.Text;
using Application.Contexts;
using Application.Extensions;
using Application.Middlewares;
using Commons.Integrations;
using Commons.Integrations.Interfaces;
using Infra.Exceptions.Mappers;
using Infra.Exceptions.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Formatting.Json;

namespace Infra.Extensions;

public static class ConfigsExtension
{
    public static void UseLogger(this IHostBuilder builder, Action<LoggerConfiguration>? configure = null)
    {
        builder.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithThreadId()
                .Enrich.WithSpan()
                .WriteTo.Console(new JsonFormatter())
                .Filter.ByExcluding(logEvent =>
                    logEvent.Properties.TryGetValue("RequestPath", out var path) &&
                    path.ToString().Contains("health")
                );

            loggerConfiguration.MinimumLevel.Information();

            configure?.Invoke(loggerConfiguration);
        });
    }

    public static void AddInfra(this IServiceCollection services)
    {
        const string swaggerVersion = "v1";
        const string swaggerTitle = "CinemaTicketSeller API";

        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddSingleton<IHttpClientProvider, HttpClientProvider>();

        services.AddCors();
        services.AddControllers().AddNewtonsoftJson();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(swaggerVersion, new OpenApiInfo()
            {
                Title = swaggerTitle,
                Version = swaggerVersion
            });
        });

        services
            .AddScoped<ErrorHandlerMiddleware>()
            .AddSingleton<ExceptionToResponseMapper>();

        services.AddContext();
        services.AuthenticationConfig();
        services.AddScoped<ErrorHandlerMiddleware>();
        services.AddServicesAndRepositories();
    }

    public static void UseInfra(this IApplicationBuilder app)
    {
        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseMiddleware<JwtMiddleware>();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    private static void AuthenticationConfig(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
        var authKey = configuration["Jwt:Key"]!;

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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    }

    private static void AddContext(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<TicketSellerContext>(
            options => options.UseNpgsql(connectionString)
        );
    }
}