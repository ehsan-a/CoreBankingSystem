using AutoMapper;
using CoreBanking.Application.DTOs.Internals;
using CoreBanking.Infrastructure.Persistence.Entities;

namespace CoreBanking.Infrastructure.Mappings
{
    public class RefreshTokenProfile : Profile
    {
        public RefreshTokenProfile()
        {
            CreateMap<RefreshToken, RefreshTokenDto>();
        }
    }
}
