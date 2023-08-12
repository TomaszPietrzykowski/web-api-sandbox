using WebApiSandbox.Data;
using WebApiSandbox.Interfaces;
using WebApiSandbox.Models;

namespace WebApiSandbox.Repository
{
    public class ProducerRepository : IProducerRepository
    {
        private readonly DataContext _context;
        public ProducerRepository(DataContext context)
        {
            _context = context;
        }

        public Producer GetProducerById(int producerId)
        {
            return _context.Producers.Where(p => p.Id == producerId).FirstOrDefault();
        }

        public ICollection<Producer> GetProducers()
        {
            return _context.Producers.OrderBy(p => p.Id).ToList();
        }

        public ICollection<Producer> GetProducersByProduct(int productId)
        {
            return _context.ProductProducers.Where(p => p.Product.Id == productId).Select(pp => pp.Producer).ToList();
        }

        public ICollection<Product> GetProductsByProducer(int producerId)
        {
            return _context.ProductProducers.Where(pp => pp.Producer.Id == producerId).Select(pp => pp.Product).ToList();
        }

        public bool ProducerExists(int producerId)
        {
            return _context.Producers.Any(p => p.Id == producerId);
        }
    }
}
