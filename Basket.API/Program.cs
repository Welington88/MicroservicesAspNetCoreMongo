using Basket.API.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var redisConnection = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");

builder.Services.AddStackExchangeRedisCache(options=> {
    options.Configuration = redisConnection;
});

builder.Services.AddScoped<IBasketRepository,BasketRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket.API", Version = "v1" })
    );

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
