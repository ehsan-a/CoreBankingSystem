using AutoMapper;
using CoreBanking.Application.DTOs.Requests.Transaction;
using CoreBanking.Application.DTOs.Responses.Transaction;
using CoreBanking.Domain.Entities;

namespace CoreBanking.Application.Mappings
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionResponseDto>();
            CreateMap<CreateTransactionRequestDto, Transaction>();
        }
    }
}
