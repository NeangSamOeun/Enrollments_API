namespace ProductWebAPI.DTOs
{
    public class PagedResult<T>
    {
        public int TotalRecords { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Items { get; set; } = new List<T>();
    }
}
