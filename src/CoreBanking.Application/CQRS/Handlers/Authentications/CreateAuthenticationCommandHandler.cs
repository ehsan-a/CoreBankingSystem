using AutoMapper;
using CoreBanking.Application.CQRS.Commands.Authentications;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.DTOs.Responses.Authentication;
using CoreBanking.Application.Exceptions;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;

namespace CoreBanking.Application.CQRS.Handlers.Authentications
{
    public class CreateAuthenticationCommandHandler : ICommandHandler<CreateAuthenticationCommand, RegisteredAuthResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAuthenticationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RegisteredAuthResponseDto> Handle(CreateAuthenticationCommand request, CancellationToken cancellationToken)
        {
            var authenticationExists = await _unitOfWork.Authentications.ExistsByNationalCodeAsync(request.NationalCode, cancellationToken);

            if (authenticationExists)
            {
                throw new ConflictException("Authentication already exists.");
            }
            var authentication = _mapper.Map<Authentication>(request);
            await _unitOfWork.Authentications.AddAsync(authentication, cancellationToken);

            Authentication.Create(authentication, request.UserId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<RegisteredAuthResponseDto>(authentication);
        }
    }
}
