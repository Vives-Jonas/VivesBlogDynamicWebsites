namespace Vives.Services.Model
{
    public class FilteredPagedServiceResult<TEntity, TFilter>:PagedServiceResult<TEntity>
    {
        public TFilter? Filter { get; set; }
    }
}
