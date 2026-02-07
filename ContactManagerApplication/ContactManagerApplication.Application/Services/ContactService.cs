using ContactManagerApplication.Application.Extensions;
using ContactManagerApplication.Application.Models;
using ContactManagerApplication.Application.Repositories;
using ContactManagerApplication.Domain;
using CsvHelper;
using Microsoft.Extensions.Logging;
using System.Formats.Asn1;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace ContactManagerApplication.Application.Services;
public class ContactService : IContactService
{
    private const int SAVING_LIMIT = 1000;
    private readonly IContactsRepository _contactsRepository;
    private readonly ILogger<ContactService> _logger;

    public ContactService(IContactsRepository contactsRepository, ILogger<ContactService> logger)
    {
        _contactsRepository = contactsRepository;
        _logger = logger;
    }

    public async Task SaveContactsToDbAsync(IAsyncEnumerable<ContactModel> contacts, CancellationToken ct)
    {
        int count = 0;

        try
        {
            await foreach (var contact in contacts)
            {
                if (++count % SAVING_LIMIT == 0) await _contactsRepository.SaveChangesAndClearTrackingAsync(ct);

                if (contact == null)
                {
                    _logger.LogWarning($"{nameof(Contact)} is null at row {count + 1}");
                }
                else
                {
                    await _contactsRepository.AddAsync(contact!.ToContact(), ct);
                }
            }
            await _contactsRepository.SaveChangesAndClearTrackingAsync(ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw;
        }
        finally
        {
            await _contactsRepository.SaveChangesAndClearTrackingAsync(ct);
            _logger.LogInformation($"Amount of saved records: {count}");
        }
    }
}
public interface IContactService
{
    Task SaveContactsToDbAsync(IAsyncEnumerable<ContactModel> contacts, CancellationToken ct);
}