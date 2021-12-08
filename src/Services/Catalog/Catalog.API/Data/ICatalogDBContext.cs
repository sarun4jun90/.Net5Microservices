﻿using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public interface ICatalogDBContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
