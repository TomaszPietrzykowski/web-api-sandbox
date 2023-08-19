using System.Linq;
using WebApiSandbox.Data;
using WebApiSandbox.Interfaces;
using WebApiSandbox.Models;

namespace WebApiSandbox.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CategoryExists(int categoryId)
        {
            return _context.Categories.Any(c => c.Id == categoryId);
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int categoryId)
        {

            return _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
        }
            
        public ICollection<Product> GetProductsByCategory(int categoryId)
        {
            return _context.ProductCategories.Where(p => p.CategoryId == categoryId).Select(c => c.Product).ToList();
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
