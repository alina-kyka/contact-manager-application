using ContactManagerApplication.Application.Repositories;
using ContactManagerApplication.Domain;
using ContactManagerApplication.Infrastructure.Context;

namespace ContactManagerApplication.Infrastructure.Repositories;
public class ContactsRepository : IRepository<Contact>
{
    public readonly ContactManagerApplicationDbContext _context;
    public ContactsRepository(ContactManagerApplicationDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Contact entity, CancellationToken ct)
    {
        await _context.AddAsync(entity);
    }

    public async Task DeleteAsync(Contact entity, CancellationToken c)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Contact>> GetAllAsync(int page, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Contact entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
