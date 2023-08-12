﻿using WebApiSandbox.Models;

namespace WebApiSandbox.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();
        Product GetProduct(int productId);
        Product GetProduct(string name);
        decimal GetProductRating(int productId);
        bool ProductExists(int productId);
    }
}
