namespace CoreBanking.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id, ISpecification<T> spec, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> ExistsAsync(int id, ISpecification<T> spec, CancellationToken cancellationToken);
    }
}
