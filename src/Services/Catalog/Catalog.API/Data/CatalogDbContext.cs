using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogDbContext:ICatalogDBContext
    {
        public CatalogDbContext(IConfiguration Configuration)
        {
            var client = new MongoClient(Configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(Configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            var Products = database.GetCollection<Product>(Configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeeds.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
