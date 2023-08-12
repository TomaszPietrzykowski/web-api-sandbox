using AutoMapper;
using WebApiSandbox.Data;
using WebApiSandbox.Dto;
using WebApiSandbox.Interfaces;
using WebApiSandbox.Models;

namespace WebApiSandbox.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;
        public CountryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CountryExists(int countryId)
        {
            return _context.Countries.Any(c => c.Id == countryId);
        }

        public IEnumerable<Country> GetCountries()
        {
            return _context.Countries.OrderBy(c => c.Id).ToList();
        }

        public Country GetCountryById(int countryId)
        {
            return _context.Countries.Where(c => c.Id == countryId).FirstOrDefault();
        }

        public Country GetProducersCountry(int producerId)
        {
            return _context.Producers.Where(p => p.Id == producerId).Select(c => c.Country).FirstOrDefault();
        }

        public IEnumerable<Producer> GetProducersByCountry(int countryId)
        {
            return _context.Producers.Where(p => p.Country.Id == countryId).ToList();
        }
    }
}
