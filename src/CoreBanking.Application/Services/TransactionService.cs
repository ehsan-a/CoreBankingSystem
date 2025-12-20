using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Transactions;
using CoreBanking.Domain.Entities;

namespace CoreBanking.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(Transaction transaction, CancellationToken cancellationToken)
        {
            await _unitOfWork.Transactions.AddAsync(transaction, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var spec = new TransactionGetAllSpec();
            var item = await _unitOfWork.Transactions.GetByIdAsync(id, spec, cancellationToken);
            if (item == null) return;
            _unitOfWork.Transactions.Delete(item);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync(CancellationToken cancellationToken)
        {
            var spec = new TransactionGetAllSpec();
            return await _unitOfWork.Transactions.GetAllAsync(spec, cancellationToken);
        }

        public async Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var spec = new TransactionGetAllSpec();
            return await _unitOfWork.Transactions.GetByIdAsync(id, spec, cancellationToken);
        }

        public async Task UpdateAsync(Transaction transaction, CancellationToken cancellationToken)
        {
            _unitOfWork.Transactions.Update(transaction);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            var spec = new TransactionGetAllSpec();
            return await _unitOfWork.Transactions.ExistsByIdAsync(id, spec, cancellationToken);
        }
    }
}
