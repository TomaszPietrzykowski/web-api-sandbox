namespace WebApiSandbox.Models
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public ICollection<ProductProducer> ProductProducer { get; set; }
    }
}
