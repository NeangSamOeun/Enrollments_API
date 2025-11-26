namespace ProductWebAPI.Models
{
    public class Menu
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public int Order { get; set; }
        public string AllowedRoles { get; set; } = string.Empty;
    }
}
