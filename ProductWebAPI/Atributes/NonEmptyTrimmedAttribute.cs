using System.ComponentModel.DataAnnotations;

namespace ProductWebAPI.Atributes;

public class NonEmptyTrimmedAttribute: ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext ctx)
    {
        var s = value as string;
        if (string.IsNullOrWhiteSpace(s))
            return new ValidationResult($"The {ctx.MemberName} field must be non-empty");
        return ValidationResult.Success;
    }
}
