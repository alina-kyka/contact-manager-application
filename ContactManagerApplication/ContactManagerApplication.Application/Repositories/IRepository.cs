namespace ContactManagerApplication.Application.Repositories;
public interface IRepository<T> where T : class
{
    Task AddAsync(T entity, CancellationToken ct);
    Task UpdateAsync(T entity, CancellationToken ct);
    Task DeleteAsync(T entity, CancellationToken ct);
    Task<IEnumerable<T>> GetAllAsync(int page, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}
