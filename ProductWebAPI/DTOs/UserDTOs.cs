namespace ProductWebAPI.DTOs
{
    public class UserDTOs
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
