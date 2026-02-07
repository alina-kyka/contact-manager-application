using ContactManagerApplication.Application.Extensions;
using ContactManagerApplication.Application.Models;
using ContactManagerApplication.Application.Repositories;
using ContactManagerApplication.Domain;
using Microsoft.Extensions.Logging;

namespace ContactManagerApplication.Application.Services;
public class ContactService : IContactService
{
    private readonly IRepository<Contact> _contactsRepository;
    private readonly ILogger<ContactService> _logger;
    private readonly ICsvService<ContactModel> _csvService;

    public ContactService(IRepository<Contact> contactsRepository, ILogger<ContactService> logger, ICsvService<ContactModel> csvService)
    {
        _contactsRepository = contactsRepository;
        _csvService = csvService;
        _logger = logger;
    }

    public async Task ImportContactsAndSaveToDbAsync(Stream stream, CancellationToken ct)
    {
        await SaveContactsToDbAsync(_csvService.ImportEntitiesFromCsvAsync(stream, ct), ct);
    }

    public async Task SaveContactsToDbAsync(IAsyncEnumerable<ContactModel> contacts, CancellationToken ct)
    {
        try
        {
            await foreach (var contact in contacts)
            {
                if (contact == null) continue;

                await _contactsRepository.AddAsync(contact!.ToContact(), ct);
            }

            await _contactsRepository.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw;
        }
    }

    public async Task UpdateAsync(int id, ContactModel contact, CancellationToken ct)
    {
        _contactsRepository.Update(contact.ToContact(id));

        await _contactsRepository.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        _contactsRepository.Delete(await _contactsRepository.GetByIdAsync(id));
        await _contactsRepository.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<ContactViewModel>> GetAllAsync(int page, int amount, CancellationToken ct)
    {
        var contacts = await _contactsRepository.GetAllAsync(page, amount, ct);

        return contacts.Select(x => x.ToContactViewModel()).ToList();
    }
}
public interface IContactService
{
    Task ImportContactsAndSaveToDbAsync(Stream stream, CancellationToken ct);
    Task<IEnumerable<ContactViewModel>> GetAllAsync(int page, int amount, CancellationToken ct);
    Task UpdateAsync(int id, ContactModel contact, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);
    Task SaveContactsToDbAsync(IAsyncEnumerable<ContactModel> contacts, CancellationToken ct);
}