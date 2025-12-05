using ProductWebAPI.Atributes;
using System.ComponentModel.DataAnnotations;

namespace ProductWebAPI.DTOs
{
    public class MajorCreateUpdateDto
    {
        [NonEmptyTrimmed, StringLength(100)]
        public string MajorName { get; set; } = default!;
    }
}
