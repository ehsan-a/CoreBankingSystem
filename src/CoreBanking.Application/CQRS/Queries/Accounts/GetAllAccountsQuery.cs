using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.DTOs.Responses.Account;
using FluentValidation;

namespace CoreBanking.Application.CQRS.Queries.Accounts
{
    public class GetAllAccountsQuery : IQuery<IEnumerable<AccountResponseDto>>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
    }

    public class GetAllAccountsQueryValidator : AbstractValidator<GetAllAccountsQuery>
    {
        public GetAllAccountsQueryValidator()
        {
            RuleFor(x => x.Limit)
             .GreaterThan(0)
             .LessThanOrEqualTo(25);

            RuleFor(x => x.Offset)
                .GreaterThanOrEqualTo(0);
        }
    }
}
