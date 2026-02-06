using CsvHelper.Configuration.Attributes;


namespace ContactManagerApplication.Application.Models;
public record ContactModel
{
    public string Name { get; set; }
    [Name("Date of birth")]
    public DateTime DateOfBirth { get; set; }
    public bool Married { get; set; }
    public string Phone { get; set; }
    public decimal Salary { get; set; }
}
