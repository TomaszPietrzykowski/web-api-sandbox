namespace WebApiSandbox.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Review> Reviews { get; set; } 
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<ProductProducer> ProductProducers { get; set; }

        public Product()
        {
            this.CreatedAt = DateTime.UtcNow;
        }
    }
}
