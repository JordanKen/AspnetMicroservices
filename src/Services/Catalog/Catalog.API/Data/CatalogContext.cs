using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data;

public class CatalogContext: ICatalogContext
{
    public CatalogContext(IConfiguration configuration)
    {
        var clients = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
        var databases = configuration.GetValue<string>("DatabaseSettings:DatabaseName");
        var client = new MongoClient(clients);
        var database = client.GetDatabase(databases);

        Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        CatalogContextSeed.SeedData(Products);
    }

    public IMongoCollection<Product> Products { get; }
}