using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogDBContext iCatalogDbContext;

        public ProductRepository(ICatalogDBContext _iCatalogDbContext)
        {
            this.iCatalogDbContext = _iCatalogDbContext ?? throw new ArgumentNullException(nameof(_iCatalogDbContext));
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await iCatalogDbContext.Products.Find(p => true).ToListAsync();
        }
        public async Task<Product> GetProduct(string id)
        {
            return await iCatalogDbContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name , name);
            return await iCatalogDbContext.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, category);
            return await iCatalogDbContext.Products.Find(filter).ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await iCatalogDbContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
           var updateresult =  await iCatalogDbContext.Products.ReplaceOneAsync(filter: g=> g.Id == product.Id,replacement : product);
           return updateresult.IsAcknowledged && updateresult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Id, id);
            var deletedResult = await iCatalogDbContext.Products.DeleteOneAsync(filter);
            return deletedResult.IsAcknowledged && deletedResult.DeletedCount > 0;
        }

      
    }
}
