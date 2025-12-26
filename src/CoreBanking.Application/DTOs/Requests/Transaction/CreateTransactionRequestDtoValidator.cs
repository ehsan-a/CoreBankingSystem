using CoreBanking.Application.DTOs.Requests.Authentication;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.DTOs.Requests.Transaction
{
    public class CreateTransactionRequestDtoValidator : AbstractValidator<CreateTransactionRequestDto>
    {
        public CreateTransactionRequestDtoValidator()
        {
            RuleFor(x => x.DebitAccountId)
       .NotEmpty();

            RuleFor(x => x.CreditAccountId)
       .NotEmpty();

        }
    }
}
