using LogiTrack.OrderService.Application.Interfaces;
using LogiTrack.OrderService.Application.Services;
using LogiTrack.OrderService.Infrastructure.Clients;
using LogiTrack.OrderService.Infrastructure.Data;
using LogiTrack.OrderService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("OrderDb")));

builder.Services.AddScoped<IOrderService, OrderService>();

// Add services to the container.

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddProblemDetails();
builder.Services.AddHttpClient<IShipmentClient, ShipmentClient>(
    client =>
    {
        client.BaseAddress = new Uri(
            builder.Configuration["ShipmentService:BaseUrl"]!);

        client.Timeout = TimeSpan.FromSeconds(10);
    });


var app = builder.Build();
app.UseExceptionHandler();
// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
