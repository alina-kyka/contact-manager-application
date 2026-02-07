namespace ContactManagerApplication.Application.Repositories;
public interface IRepository<T> where T : class
{
    Task AddAsync(T entity, CancellationToken ct);
    void Update(T entity);
    void Delete(T entity);
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync(int page,int amount, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}
