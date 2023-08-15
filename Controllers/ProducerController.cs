using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiSandbox.Dto;
using WebApiSandbox.Interfaces;
using WebApiSandbox.Models;
using WebApiSandbox.Repository;

namespace WebApiSandbox.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : Controller
    {
        private readonly IProducerRepository _producerRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;  
        public ProducerController(IProducerRepository producerRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _producerRepository = producerRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;   
        }

        [HttpGet("{producerId}")]
        [ProducesResponseType(200, Type = typeof(Producer))]
        [ProducesResponseType(400)]
        public IActionResult GetProducers(int producerId)
        {
            if (!_producerRepository.ProducerExists(producerId))
                return NotFound();

            var producer = _mapper.Map<ProducerDto>(_producerRepository.GetProducerById(producerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(producer);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Producer>))]
        [ProducesResponseType(400)]
        public IActionResult GetProducers()
        {
            var producers = _mapper.Map<List<ProducerDto>>(_producerRepository.GetProducers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(producers);

        }

        [HttpGet("product/{productId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Producer>))]
        [ProducesResponseType(400)]
        public IActionResult GetProducersByProduct(int productId)
        {
            var producers = _mapper.Map<List<ProducerDto>>(_producerRepository.GetProducersByProduct(productId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(producers);

        }

        [HttpGet("{producerId}/product")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(400)]
        public IActionResult GetProductsByProducer(int producerId)
        {
            if (!_producerRepository.ProducerExists(producerId))
                return NotFound();

            var products = _mapper.Map<List<ProductDto>>(_producerRepository.GetProductsByProducer(producerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateProducer([FromBody] ProducerDto producerCreate, [FromQuery] int countryId)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Controller entered");
            if (producerCreate == null)
                return BadRequest(ModelState);

            var producer = _producerRepository.GetProducers()
                .Where(c => c.Name.Trim().ToUpper() == producerCreate.Name.ToUpper())
                .FirstOrDefault();

            if (producer != null)
            {
                ModelState.AddModelError("", "Producer already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var producerMap = _mapper.Map<Producer>(producerCreate);

            producerMap.Country = _countryRepository.GetCountryById(countryId);

            if (!_producerRepository.CreateProducer(producerMap))
            {
                ModelState.AddModelError("", "Something went wrong saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");

        }
    }
}
