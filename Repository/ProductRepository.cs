using WebApiSandbox.Data;
using WebApiSandbox.Interfaces;
using WebApiSandbox.Models;

namespace WebApiSandbox.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateProduct(int producerId, int categoryId, Product product)
        {
            var producer = _context.Producers.Where(p => p.Id == producerId).FirstOrDefault();
            var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            var productProducer = new ProductProducer()
            {
                Producer = producer,
                Product = product
            };

            _context.Add(productProducer);

            var productCategory = new ProductCategory()
            {
                Category = category,
                Product = product
            };

            _context.Add(productCategory);

            _context.Add(product);

            return Save();
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public Product GetProduct(string name)
        {
            return _context.Products.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetProductRating(int productId)
        {
            var reviews = _context.Reviews.Where(r => r.Product.Id == productId);
            if (reviews.Count() == 0) 
                return 0;
            return ((decimal)reviews.Sum(r => r.Rating) / reviews.Count());
        }

        public ICollection<Product> GetProducts()
        {
            return _context.Products.OrderBy(p => p.Id).ToList();
        }

        public bool ProductExists(int productId)
        {
            return _context.Products.Any(p => p.Id == productId);
        }

        public bool UpdateProduct(int producerId, int categoryId, Product product)
        {
            _context.Update(product);
            return Save();
        }

        public bool DeleteProduct(Product product)
        {
            _context.Remove(product);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
