using AutoMapper;
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
        }
    }
}
