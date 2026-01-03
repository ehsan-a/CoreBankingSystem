using AutoMapper;
using CoreBanking.Application.CQRS.Commands.Accounts;
using CoreBanking.Application.CQRS.Commands.Authentications;
using CoreBanking.Application.DTOs.Requests.Account;
using CoreBanking.Application.DTOs.Requests.Authentication;
using CoreBanking.Application.DTOs.Responses.Authentication;
using CoreBanking.Domain.Entities;

namespace CoreBanking.Application.Mappings
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<Authentication, RegisteredAuthResponseDto>();
            CreateMap<CreateAuthenticationRequestDto, Authentication>();

            CreateMap<CreateAuthenticationCommand, Authentication>();
            CreateMap<CreateAuthenticationRequestDto, CreateAuthenticationCommand>();
        }
    }
}
