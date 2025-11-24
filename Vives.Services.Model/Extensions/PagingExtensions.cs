namespace Vives.Services.Model.Extensions
{
    public static class PagingExtensions
    {
        public static IQueryable<T> ApplyPaging<T>(this IOrderedQueryable<T> query, Paging paging)
        {
            if (paging.Offset < 0)
            {
                paging.Offset = 0;
            }

            if(paging.Limit <= 0)
            {
                paging.Limit = 10;
            }

            if (paging.Limit > 1000)
            {
                paging.Limit = 1000;
            }

            return query
                .Skip(paging.Offset)
                .Take(paging.Limit);
        }
    }
}
