using MercadoPago.Config;
using Microsoft.EntityFrameworkCore;
using StackFood.Application;
using StackFood.Application.Interfaces.Services;
using StackFood.Application.Services;
using StackFood.ExternalService.MercadoPago;
using StackFood.Infra;
using StackFood.Infra.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
MercadoPagoConfig.AccessToken = "APP_USR-3012794291586434-051711-5fe595076a0027ab8a6be1bde5cd28f7-709468526";
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

MercadoPagoBootstrapper.Register(builder.Services);
InfraBootstrapper.Register(builder.Services);
ApplicationBootstrapper.Register(builder.Services);

builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

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
