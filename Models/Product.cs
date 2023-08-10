﻿namespace WebApiSandbox.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PriceInCents { get; set; } 
    }
}