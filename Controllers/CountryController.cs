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
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountryById(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
                return NotFound();

            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryById(countryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(country);
        }

        [HttpGet("producer/{producerId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetProducersCountry(int producerId)
        {

            var country = _mapper.Map<CountryDto>(_countryRepository.GetProducersCountry(producerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(country);
        }


        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] CountryDto countryCreate)
        {
            if (countryCreate == null)
                return BadRequest(ModelState);

            var country = _countryRepository.GetCountries()
                .Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.ToUpper())
                .FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryMap = _mapper.Map<Country>(countryCreate);

            if (!_countryRepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");

        }
    }
}
