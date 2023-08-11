using WebApiSandbox.Data;
using WebApiSandbox.Models;

namespace WebApiSandbox
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.ProductProducers.Any())
            {
                var productProducers = new List<ProductProducer>()
                {
                    new ProductProducer()
                    {
                        Product = new Product()
                        {
                            Name = "Amazing Product",
                            CreatedAt = new DateTime(1903,1,1),
                            ProductCategories = new List<ProductCategory>()
                            {
                                new ProductCategory { Category = new Category() { Name = "Electric"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Amazing Product",Text = "Product is the best pokemon, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Amazing Product", Text = "Product is the best at killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Amazing Product",Text = "Product not really that good", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Producer = new Producer()
                        {
                            Name = "Jack",
                            Industry = "London",
                            Slogan = "Mistys Slogan",
                            Country = new Country()
                            {
                                Name = "Kanto"
                            }
                        }
                    },
                    new ProductProducer()
                    {
                        Product = new Product()
                        {
                            Name = "Squirtle",
                            CreatedAt = new DateTime(1903,1,1),
                            ProductCategories = new List<ProductCategory>()
                            {
                                new ProductCategory { Category = new Category() { Name = "Water"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title= "Squirtle", Text = "squirtle is the best pokemon, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title= "Squirtle",Text = "Squirtle is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title= "Squirtle", Text = "squirtle, squirtle, squirtle", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Producer = new Producer()
                        {
                            Name = "Harry",
                            Industry = "Potter",
                            Slogan = "Harry obite wary",
                            Country = new Country()
                            {
                                Name = "Saffron City"
                            }
                        }
                    },
                                    new ProductProducer()
                    {
                        Product = new Product()
                        {
                            Name = "Venasuar",
                            CreatedAt = new DateTime(1903,1,1),
                            ProductCategories = new List<ProductCategory>()
                            {
                                new ProductCategory { Category = new Category() { Name = "Leaf"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Veasaur",Text = "Venasuar is the best pokemon, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Veasaur",Text = "Venasuar is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Veasaur",Text = "Venasuar, Venasuar, Venasuar", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Producer = new Producer()
                        {
                           Name = "Ash",
                            Industry = "Ketchum",
                            Slogan = "Ashs Slogan",
                            Country = new Country()
                            {
                                Name = "Millet Town"
                            }
                        }
                    }
                };
                dataContext.ProductProducers.AddRange(productProducers);
                dataContext.SaveChanges();
            }
        }
    }
}
