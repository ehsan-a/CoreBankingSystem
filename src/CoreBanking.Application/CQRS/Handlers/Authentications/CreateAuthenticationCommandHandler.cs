using AutoMapper;
using CoreBanking.Application.CQRS.Commands.Authentications;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.DTOs.Responses.Authentication;
using CoreBanking.Application.Exceptions;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Application.CQRS.Handlers.Authentications
{
    public class CreateAuthenticationCommandHandler : ICommandHandler<CreateAuthenticationCommand, RegisteredAuthResponseDto>
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IMapper _mapper;

        public CreateAuthenticationCommandHandler(IAuthenticationRepository authenticationRepository, IMapper mapper)
        {
            _authenticationRepository = authenticationRepository;
            _mapper = mapper;
        }

        public async Task<RegisteredAuthResponseDto> Handle(CreateAuthenticationCommand request, CancellationToken cancellationToken)
        {
            var authenticationExists = await _authenticationRepository.ExistsByNationalCodeAsync(request.NationalCode, cancellationToken);

            if (authenticationExists)
            {
                throw new ConflictException("Authentication already exists.");
            }
            var authentication = Authentication.Create(
                request.NationalCode,
                request.CentralBankCreditCheckPassed,
                request.CivilRegistryVerified,
                request.PoliceClearancePassed,
                request.UserId);

            await _authenticationRepository.AddAsync(authentication, cancellationToken);

            await _authenticationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return _mapper.Map<RegisteredAuthResponseDto>(authentication);
        }
    }
}
