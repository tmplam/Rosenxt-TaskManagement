using TaskManagement.API;
using TaskManagement.Application;
using TaskManagement.Infrastructure;
using TaskManagement.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApiServices()
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddPersistenceServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApiServices();

app.Run();
