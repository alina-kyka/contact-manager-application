using ContactManagerApplication.Application.Repositories;
using ContactManagerApplication.Domain;
using ContactManagerApplication.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

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
        await _context.Contacts.AddAsync(entity);
    }

    public void DeleteAsync(Contact entity, CancellationToken c)
    {
        _context.Contacts.Remove(entity);
    }

    public async Task<IEnumerable<Contact>> GetAllAsync(int page, int amount, CancellationToken ct)
    {
        return await _context.Contacts
            .OrderBy(x => x.Id)
            .Skip(page * amount)
            .Take(amount)
            .ToListAsync(ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _context.SaveChangesAsync(ct);
    }

    public void UpdateAsync(Contact entity, CancellationToken ct)
    {
        _context.Contacts.Update(entity);
    }
}
