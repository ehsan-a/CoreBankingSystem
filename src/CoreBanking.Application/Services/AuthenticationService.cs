using AutoMapper;
using CoreBanking.Application.CQRS.Commands.Authentications;
using CoreBanking.Application.CQRS.Queries.Authentications;
using CoreBanking.Application.DTOs.Requests.Authentication;
using CoreBanking.Application.DTOs.Responses.Authentication;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CoreBanking.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IMapper _mapper;
        private readonly IValidator<CreateAuthenticationRequestDto> _validator;
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public AuthenticationService(IMapper mapper, IValidator<CreateAuthenticationRequestDto> validator, UserManager<User> userManager, IMediator mediator)
        {
            _mapper = mapper;
            _validator = validator;
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<RegisteredAuthResponseDto> CreateAsync(CreateAuthenticationRequestDto createAuthenticationRequestDto, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(createAuthenticationRequestDto);
            var user = await _userManager.GetUserAsync(principal);
            var command = _mapper.Map<CreateAuthenticationCommand>(createAuthenticationRequestDto);
            command.UserId = user.Id;
            return await _mediator.Send(command);
        }

        public async Task<AuthenticationResponseDto?> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAuthenticationByNationalCodeQuery { NationalCode = nationalCode });
        }
        public async Task<bool> ExistsAsync(string nationalCode, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAuthenticationExistsQuery { NationalCode = nationalCode });
        }
        public async Task<AuthenticationResponseDto> GetInquiryAsync(string nationalCode, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetInquiryAuthenticationQuery { NationalCode = nationalCode });
        }
    }
}
