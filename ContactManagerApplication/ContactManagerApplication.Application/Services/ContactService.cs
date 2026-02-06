using ContactManagerApplication.Application.Extensions;
using ContactManagerApplication.Application.Models;
using ContactManagerApplication.Application.Repositories;
using ContactManagerApplication.Domain;
using CsvHelper;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace ContactManagerApplication.Application.Services;
public class ContactService : IContactService
{
    private const int SAVING_LIMIT = 1000;
    private readonly IRepository<Contact> _contactsRepository;
    private readonly ILogger<ContactService> _logger;

    public ContactService(IRepository<Contact> contactsRepository, ILogger<ContactService> logger)
    {
        _contactsRepository = contactsRepository;
        _logger = logger;
    }
    public async Task ImportContactsFromCsvAsync(Stream stream, CancellationToken ct)
    {
        using var csvReader = new CsvReader(new StreamReader(stream), CultureInfo.InvariantCulture);

        int count = 0;

        try
        {
            await foreach (var record in csvReader.GetRecordsAsync<ContactModel>(ct))
            {
                if (++count % SAVING_LIMIT == 0) await _contactsRepository.SaveChangesAsync(ct);

                if (record == null)
                {
                    _logger.LogWarning($"{nameof(Contact)} is null at row {count}");
                }
                else
                {
                    await _contactsRepository.AddAsync(record!.ToContact(), ct);
                }
            }
            await _contactsRepository.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }
        finally
        {
            _logger.LogInformation($"Amount of saved records: {count}");
        }
    }
}
public interface IContactService
{
    Task ImportContactsFromCsvAsync(Stream stream, CancellationToken ct);
}