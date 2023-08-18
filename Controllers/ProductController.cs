using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiSandbox.Dto;
using WebApiSandbox.Interfaces;
using WebApiSandbox.Models;
using WebApiSandbox.Repository;

namespace WebApiSandbox.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetProducts()
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProducts());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public IActionResult GetProduct(int productId)
        {
            if (!_productRepository.ProductExists(productId))
                return NotFound();

            var product = _mapper.Map<ProductDto>(_productRepository.GetProduct(productId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        [HttpGet("{productId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetProductRating(int productId)
        {
            if (!_productRepository.ProductExists(productId))
                return NotFound();

            var productRating = _productRepository.GetProductRating(productId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(productRating);

        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateProduct([FromBody] ProductDto productCreate, [FromQuery] int categoryId, [FromQuery] int producerId)
        {
            if (productCreate == null)
                return BadRequest(ModelState);

            var product = _productRepository.GetProducts()
                .Where(p => p.Name.Trim().ToUpper() == productCreate.Name.ToUpper())
                .FirstOrDefault();
            // rethink this condition in case of product
            if (product != null)
            {
                ModelState.AddModelError("", "Producer already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(productCreate);


            if (!_productRepository.CreateProduct(producerId, categoryId, productMap))
            {
                ModelState.AddModelError("", "Something went wrong saving");
                return StatusCode(500, ModelState);
            }

            var newProduct = _mapper.Map<ProductDto>(productMap);

            return Created(productMap.Id.ToString(), newProduct);

        }
    }
}
