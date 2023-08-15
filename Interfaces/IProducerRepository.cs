using WebApiSandbox.Models;

namespace WebApiSandbox.Interfaces
{
    public interface IProducerRepository
    {
        ICollection<Producer> GetProducers();
        Producer GetProducerById(int producerId);
        ICollection<Producer> GetProducersByProduct(int productId);
        ICollection<Product> GetProductsByProducer(int producerId);
        bool ProducerExists(int producerId);
        bool CreateProducer(Producer producer);
        bool Save();
    }
}
