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
            CreateMap<ProductDto, Product>();
            // When there is no need for different mapping
            // on write and read:
            // CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();

            CreateMap<Producer, ProducerDto>();
            CreateMap<ProducerDto, Producer>();

            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();

            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ReviewerDto, Reviewer>();
        }
    }
}
