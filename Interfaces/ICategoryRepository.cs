﻿using WebApiSandbox.Models;

namespace WebApiSandbox.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int categoryId);
        ICollection<Product> GetProductsByCategory(int categoryId);
        bool CategoryExist(int categoryId);
    }
}