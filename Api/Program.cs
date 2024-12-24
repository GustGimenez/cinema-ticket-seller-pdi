using Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLogger();
builder.Services.AddInfra();

var app = builder.Build();

app.UseInfra();

app.Run();