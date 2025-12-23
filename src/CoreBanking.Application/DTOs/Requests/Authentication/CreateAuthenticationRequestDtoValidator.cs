using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.DTOs.Requests.Authentication
{
    public class CreateAuthenticationRequestDtoValidator : AbstractValidator<CreateAuthenticationRequestDto>
    {
        public CreateAuthenticationRequestDtoValidator()
        {
            RuleFor(x => x.NationalCode)
       .NotEmpty()
       .MinimumLength(10);

        }
    }
}
