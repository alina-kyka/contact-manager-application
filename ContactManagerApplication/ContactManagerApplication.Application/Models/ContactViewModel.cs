using CsvHelper.Configuration.Attributes;

namespace ContactManagerApplication.Application.Models;
public record ContactViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    [Name("Date of birth")]
    public DateTime DateOfBirth { get; set; }
    public bool Married { get; set; }
    public string Phone { get; set; }
    public decimal Salary { get; set; }
}
