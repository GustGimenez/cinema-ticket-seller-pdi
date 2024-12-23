using cinema_ticket_seller_pdi.Configs;
using cinema_ticket_seller_pdi.Contexts;
using cinema_ticket_seller_pdi.Filters;
using cinema_ticket_seller_pdi.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Filters.Add(new ExceptionFilter()));
builder.Services.AddDbContext<TicketSellerContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ServiceRegister.AddServicesAuthentication(builder.Services, builder.Configuration["Jwt:Key"]);
ServiceRegister.RegisterServices(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();