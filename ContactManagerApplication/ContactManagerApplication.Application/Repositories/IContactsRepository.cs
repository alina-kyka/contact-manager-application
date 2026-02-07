using ContactManagerApplication.Application.Repositories.Base;
using ContactManagerApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerApplication.Application.Repositories;
public interface IContactsRepository: IRepository<Contact>
{
    Task SaveChangesAndClearTrackingAsync(CancellationToken ct);
}
