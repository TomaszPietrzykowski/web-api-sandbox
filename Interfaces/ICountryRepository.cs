using WebApiSandbox.Models;

namespace WebApiSandbox.Interfaces
{
    public interface ICountryRepository
    {
        Country GetCountryById(int countryId);
        Country GetProducersCountry(int producerId);
        IEnumerable<Country> GetCountries();
        IEnumerable<Producer> GetProducersByCountry(int countryId);
        bool CountryExists(int countryId);
        bool CreateCountry(Country country);
        bool Save();
    }
}
