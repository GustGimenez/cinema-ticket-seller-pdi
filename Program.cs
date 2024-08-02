using cinema_ticket_seller_pdi.Configs;
using cinema_ticket_seller_pdi.Contexts;
using cinema_ticket_seller_pdi.Filters;
using cinema_ticket_seller_pdi.Repositories;
using cinema_ticket_seller_pdi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Filters.Add(new ExceptionFilter()));
builder.Services.AddDbContext<TicketSellerContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IoCConfig.RegisterServices(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
