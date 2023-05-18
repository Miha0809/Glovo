using AutoMapper;
using Company.Models;
using Company.Models.Dtos;

namespace Glovo.Services;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Order, OrderDto>();
        CreateMap<Product, ProductDto>();
        CreateMap<Client.Models.Client, Client.Models.Dtos.ClientDto>();
        CreateMap<Company.Models.Company, CompanyDto>();
    }
}