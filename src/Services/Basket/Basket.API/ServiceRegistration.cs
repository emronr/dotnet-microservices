using Microsoft.OpenApi.Models;

namespace Basket.API;

public static class ServiceRegistration
{
    public static void AddApiRegistration(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddControllers();
        service.AddEndpointsApiExplorer();
        service.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Basket.API",
                Version = "v1"
            });
        });
        service.AddStackExchangeRedisCache( options =>
        {
            options.Configuration = configuration.GetSection("CacheSettings:ConnectionString").Value;
        });

    }
}