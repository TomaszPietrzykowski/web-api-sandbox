﻿namespace WebApiSandbox.Models
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }
        public string Slogan { get; set; }
        public Country Country { get; set; }
        public ICollection<ProductProducer> ProductProducers { get; set; }
    }
}
