namespace WebApiSandbox.Models
{
    public class ProductProducer
    {
        public int ProductId { get; set; }
        public int ProducerId { get; set; }
        public Product Product { get; set; }
        public Producer Producer { get; set; }
    }
}
// not really many-to-many, doesn't make sense
// or does it