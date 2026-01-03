using AutoMapper;
using CoreBanking.Application.CQRS.Commands.Accounts;
using CoreBanking.Application.DTOs.Requests.Account;
using CoreBanking.Application.DTOs.Responses.Account;
using CoreBanking.Domain.Entities;

namespace CoreBanking.Application.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountResponseDto>();
            CreateMap<CreateAccountRequestDto, Account>();
            CreateMap<UpdateAccountRequestDto, Account>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateAccountCommand, Account>();
            CreateMap<UpdateAccountCommand, Account>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CreateAccountRequestDto, CreateAccountCommand>();
            CreateMap<UpdateAccountRequestDto, UpdateAccountCommand>();
        }
    }
}
