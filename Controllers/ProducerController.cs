﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiSandbox.Dto;
using WebApiSandbox.Interfaces;
using WebApiSandbox.Models;

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

            var newProducer = _mapper.Map<ProducerDto>(producerMap);

            return Created(producerMap.Id.ToString(), newProducer);
        }

        [HttpPut("{producerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProducer(int producerId, [FromBody] ProducerDto producerUpdate)
        {
            if (producerUpdate == null)
                return BadRequest(ModelState);

            if (producerId != producerUpdate.Id)
                return BadRequest(ModelState);

            if (!_producerRepository.ProducerExists(producerId))
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var producerMap = _mapper.Map<Producer>(producerUpdate);

            if (!_producerRepository.UpdateProducer(producerMap))
            {
                ModelState.AddModelError("", "Something went wrong saving");
                return StatusCode(500, ModelState);
            }

            var newProducer = _mapper.Map<ProducerDto>(producerMap);

            return Ok(newProducer);

        }

        [HttpDelete("{producerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult DeleteProducer(int producerId)
        {
            if (!_producerRepository.ProducerExists(producerId))
            {
                return NotFound();
            }

            // placeholder check: handle potentially breaking relation before delete:
            var dependantProducts = _producerRepository.GetProductsByProducer(producerId);

            if (dependantProducts.Any())
            {
                //return BadRequest(ModelState);
                //// perhaps return the list of dependant producers Ids
                var ids = dependantProducts.Select(p => p.Id);
                return UnprocessableEntity(ids);
            }

            var producerToBeDeletd = _producerRepository.GetProducerById(producerId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_producerRepository.DeleteProducer(producerToBeDeletd))
            {
                ModelState.AddModelError("", "Something went wrong deleting producer");
            }

            return NoContent();
        }
    }
}
