using Catalog.API.Data;
using Catalog.API.Repositories;
using Microsoft.OpenApi.Models;

namespace Catalog.API;

public static class ServiceRegistration
{
    public static void AddApiRegistration(this IServiceCollection service)
    {
        service.AddControllers();
        service.AddEndpointsApiExplorer();
        service.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Catalog.API",
                Version = "v1"
            });
        });

        service.AddScoped<ICatalogContext, CatalogContext>();
        service.AddScoped<IProductRepository, ProductRepository>();
    }
}