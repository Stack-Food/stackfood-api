using MercadoPago.Config;
using Microsoft.EntityFrameworkCore;
using StackFood.Application;
using StackFood.Infra.ExternalService.MercadoPago;
using StackFood.Infra;
using StackFood.Infra.Persistence;
using StackFood.Infra.Persistence.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using StackFood.API.Validators.Customer;
using StackFood.Application.UseCases.Customers.Create;
using StackFood.Application.UseCases.Customers.GetByCpf;
using StackFood.Application.Interfaces.Repositories;
using StackFood.Application.UseCases.Products.Create;
using StackFood.Application.UseCases.Products.Delete;
using StackFood.Application.UseCases.Products.GetAll;
using StackFood.Application.UseCases.Products.GetById;
using StackFood.Application.UseCases.Products.Update;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
MercadoPagoConfig.AccessToken = "APP_USR-3012794291586434-051711-5fe595076a0027ab8a6be1bde5cd28f7-709468526";
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerRequestValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

MercadoPagoBootstrapper.Register(builder.Services);
InfraBootstrapper.Register(builder.Services);
ApplicationBootstrapper.Register(builder.Services);

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
