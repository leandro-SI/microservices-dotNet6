using AutoMapper;
using LeoShopping.ProductAPI.Config;
using LeoShopping.ProductAPI.Model.Context;
using LeoShopping.ProductAPI.Repository;
using LeoShopping.ProductAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var connection = builder.Configuration["ConnectionStrings:LeoConnectionDB"];

builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 32))));

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "LeoShopping Microservices Project", 
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Leandro Cesar",
            Email = "contato@leandrocesar.com",
            Url = new Uri("http://www.leandrocesar.com.br"),
            
        }
    
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
