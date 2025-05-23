using MercadoPago.Config;
using Microsoft.EntityFrameworkCore;
using StackFood.Application;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.Interfaces.Services;
using StackFood.Application.Services;
using StackFood.Infra;
using StackFood.Infra.Persistence;
using StackFood.Infra.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
MercadoPagoConfig.AccessToken = "TEST-3012794291586434-051711-8a400aab6fd7b21510de463589bbd8a1-709468526";
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

InfraBootstrapper.Register(builder.Services);
ApplicationBootstrapper.Register(builder.Services);

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

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

