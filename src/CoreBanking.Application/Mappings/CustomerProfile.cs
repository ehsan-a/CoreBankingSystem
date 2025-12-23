using AutoMapper;
using CoreBanking.Application.DTOs.Requests.Account;
using CoreBanking.Application.DTOs.Requests.Customer;
using CoreBanking.Application.DTOs.Responses.Account;
using CoreBanking.Application.DTOs.Responses.Customer;
using CoreBanking.Domain.Entities;

namespace CoreBanking.Application.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerResponseDto>();
            CreateMap<CreateCustomerRequestDto, Customer>();
            CreateMap<UpdateCustomerRequestDto, Customer>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
