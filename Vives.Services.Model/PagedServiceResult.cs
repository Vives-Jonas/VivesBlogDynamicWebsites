namespace Vives.Services.Model
{

    public class PagedServiceResult<T> : ServiceResult<IEnumerable<T>>
    {
        public int TotalCount { get; set; }
        public Paging Paging { get; set; } = new Paging();
        public string? Sorting { get; set; }

    }
}
