using AutoMapper;
using WebApiSandbox.Dto;
using WebApiSandbox.Models;

namespace WebApiSandbox.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<Producer, ProducerDto>();
            CreateMap<Review, ReviewDto>();
        }
    }
}
