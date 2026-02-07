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

    public static Contact ToContact(this ContactModel model, int id)
    {
        return new Contact
        {
            Id = id,
            Name = model.Name,
            Phone = model.Phone,
            DateOfBirth = model.DateOfBirth,
            Married = model.Married,
            Salary = model.Salary,
        };
    }

    public static ContactModel ToContactModel(this Contact entity)
    {
        return new ContactModel()
        {
            Name = entity.Name,
            Phone = entity.Phone,
            DateOfBirth = entity.DateOfBirth,
            Married = entity.Married,
            Salary = entity.Salary,
        };
    }

    public static Contact ToContact(this ContactViewModel model)
    {
        return new Contact
        {
            Id = model.Id,
            Name = model.Name,
            Phone = model.Phone,
            DateOfBirth = model.DateOfBirth,
            Married = model.Married,
            Salary = model.Salary,
        };
    }
    public static ContactViewModel ToContactViewModel(this Contact model)
    {
        return new ContactViewModel
        {
            Id = model.Id,
            Name = model.Name,
            Phone = model.Phone,
            DateOfBirth = model.DateOfBirth,
            Married = model.Married,
            Salary = model.Salary,
        };
    }
}
