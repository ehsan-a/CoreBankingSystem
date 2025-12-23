using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Accounts;
using CoreBanking.Application.Exceptions;
using CoreBanking.Domain.Entities;

namespace CoreBanking.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INumberGenerator _numberGenerator;
        public AccountService(IUnitOfWork unitOfWork, INumberGenerator numberGenerator)
        {
            _unitOfWork = unitOfWork;
            _numberGenerator = numberGenerator;
        }
        public async Task CreateAsync(Account account, CancellationToken cancellationToken)
        {
            var customerExists = await _unitOfWork.Customers.ExistsByIdAsync(account.CustomerId, cancellationToken);

            if (!customerExists)
            {
                throw new NotFoundException("Customer", account.CustomerId);
            }
            account.AccountNumber = await _numberGenerator.GenerateAccountNumberAsync();
            await _unitOfWork.Accounts.AddAsync(account, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var spec = new AccountGetAllSpec();
            var item = await _unitOfWork.Accounts.GetByIdAsync(id, spec, cancellationToken);
            if (item == null) return;
            _unitOfWork.Accounts.Delete(item);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Account>> GetAllAsync(CancellationToken cancellationToken)
        {
            var spec = new AccountGetAllSpec();
            return await _unitOfWork.Accounts.GetAllAsync(spec, cancellationToken);
        }

        public async Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var spec = new AccountGetAllSpec();
            return await _unitOfWork.Accounts.GetByIdAsync(id, spec, cancellationToken);
        }

        public async Task UpdateAsync(Account account, CancellationToken cancellationToken)
        {
            _unitOfWork.Accounts.Update(account);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            var spec = new AccountGetAllSpec();
            return await _unitOfWork.Accounts.ExistsByIdAsync(id, spec, cancellationToken);
        }
    }
}
