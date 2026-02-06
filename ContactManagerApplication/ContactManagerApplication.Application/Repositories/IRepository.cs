namespace ContactManagerApplication.Application.Repositories;
public interface IRepository<T> where T : class
{
    Task AddAsync(T entity, CancellationToken ct);
    void UpdateAsync(T entity, CancellationToken ct);
    void DeleteAsync(T entity, CancellationToken ct);
    Task<IEnumerable<T>> GetAllAsync(int page,int amount, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}
