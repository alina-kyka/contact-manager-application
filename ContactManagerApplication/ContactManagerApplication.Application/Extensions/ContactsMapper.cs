using ContactManagerApplication.Application.Models;
using ContactManagerApplication.Domain;

namespace ContactManagerApplication.Application.Extensions;
public static class ContactsMapper
{
    public static Contact ToContact(this ContactModel model)
    {
        return new Contact
        {
            Name = model.Name,
            Phone = model.Phone,
            DateOfBirth = model.DateOfBirth,
            Married = model.Married,
            Salary = model.Salary,
        };
    }
}
