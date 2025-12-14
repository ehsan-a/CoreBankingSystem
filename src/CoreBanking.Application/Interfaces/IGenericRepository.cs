namespace CoreBanking.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id, ISpecification<T> spec, CancellationToken cancellationToken = default);
        Task<T> GetByNationalCodeAsync(string nationalCode, ISpecification<T> spec, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> ExistsByIdAsync(Guid id, ISpecification<T> spec, CancellationToken cancellationToken);
        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> ExistsByNationalCodeAsync(string nationalCode, ISpecification<T> spec, CancellationToken cancellationToken);
        Task<bool> ExistsByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken);
    }
}
