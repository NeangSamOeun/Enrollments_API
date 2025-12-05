namespace ProductWebAPI.DTOs;
public class StudentERMDTO
{
    public string Code { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Sex { get; set; } = default!;
    public DateTime DOB { get; set; }
    public string Nationality { get; set; } = default!;
    public string Telegram { get; set; } = default!;
    public string FatherName { get; set; } = default!;
    public string MotherName { get; set; } = default!;
}
