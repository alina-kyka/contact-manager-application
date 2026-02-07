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

    public void Delete(Contact entity)
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

    public async Task<Contact> GetByIdAsync(int id)
    {
        return await _context.Contacts.FirstAsync(x => x.Id == id);
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _context.SaveChangesAsync(ct);
    }

    public void Update(Contact entity)
    {
        _context.Contacts.Update(entity);
    }
}
