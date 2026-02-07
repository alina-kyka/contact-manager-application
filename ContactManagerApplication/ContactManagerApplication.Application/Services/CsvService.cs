using ContactManagerApplication.Application.Models;
using ContactManagerApplication.Application.Repositories;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerApplication.Application.Services;
public class CsvService: ICsvService<ContactModel>
{
    private readonly ILogger<ContactService> _logger;

    public CsvService(ILogger<ContactService> logger)
    {
        _logger = logger;
    }
    public async IAsyncEnumerable<ContactModel> ImportEntitiesFromCsvAsync(Stream stream, [EnumeratorCancellation] CancellationToken ct)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            ShouldSkipRecord = args => args.Row.Parser.Record?.All(string.IsNullOrWhiteSpace) == true,

            BadDataFound = args => 
            {
                _logger.LogError($"Format error at row {args.Context.Parser?.Row}");
            },

            ReadingExceptionOccurred = args => 
            {
                _logger.LogWarning($"Skipping row {args.Exception?.Context?.Parser?.Row} due to conversion error.");
                return false;
            }
        };

        using var csvReader = new CsvReader(new StreamReader(stream), config);

        await foreach (var record in csvReader.GetRecordsAsync<ContactModel>(ct))
        {
            yield return record;
        }
    }
}

public interface ICsvService<T>
{
    IAsyncEnumerable<T> ImportEntitiesFromCsvAsync(Stream stream, CancellationToken ct);
}
