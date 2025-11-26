namespace ProductWebAPI.DTOs
{
    public class MenuDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public int Order { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
