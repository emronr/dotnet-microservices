using System.Net;
using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _catalogContext;
    private readonly IMongoCollection<Product> _productCollection;

    public ProductRepository(ICatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
        _productCollection = catalogContext.Products;
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _productCollection
            .Find(p => true)
            .ToListAsync();
    }

    public async Task<Product> GetProduct(string id)
    {
        return await _productCollection
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> SearchProduct(string name)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

        return await _productCollection
            .Find(filter)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByCategory(string category)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, category);

        return await _productCollection
            .Find(filter)
            .ToListAsync();
    }

    public async Task CreateProduct(Product product)
    {
        await _productCollection.InsertOneAsync(product);
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var updateResult =  await _productCollection.ReplaceOneAsync(p => p.Id == product.Id, product);
        
        return updateResult.IsAcknowledged 
            && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

        var deleteResult =  await _productCollection.DeleteOneAsync(filter);
        
        return deleteResult.IsAcknowledged 
               && deleteResult.DeletedCount > 0;
    }
}