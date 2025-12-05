namespace ProductWebAPI.Models;

public class StudentERM
{
    public string StudentId { get; set; }  = Guid.NewGuid().ToString();
    public string Code { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Sex { get; set; } = default!;
    public DateTime DOB { get; set; } 
    public string Nationality { get; set; } = default!;
    public string Telegram { get; set; } = default!;
    public string FatherName { get; set; } = default!;
    public string MotherName { get; set; } = default!;

    // Navigation properties
    public CurrentEducation CurrentEducation { get; set; } = default!;
    public PermanentAddress PermanentAddress { get; set; } = default!;
    public ContactInformation ContactInformation { get; set; } = default!;
    public ICollection<RegisterInformation> RegisterInformations { get; set; } = default!;
}
