using WebApiSandbox.Models;

namespace WebApiSandbox.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();
    }
}
