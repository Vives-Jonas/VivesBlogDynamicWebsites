using VivesBlog.Dto.Filter;
using VivesBlog.Model;

namespace VivesBlog.Services.Extensions.Filters
{
    public static class PersonFilterExtensions
    {
        public static IQueryable<Person> ApplyFilter(this IQueryable<Person> query, PersonFilter? filter)
        {
            if (filter is null)
            {
                return query;
            }

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                var searchCriteria = filter.Search.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var searchText in searchCriteria)
                {


                    var loweredSearchText = searchText.ToLowerInvariant();
                    query = query.Where(person =>
                        person.FirstName.ToLower().Contains(loweredSearchText)
                        || person.LastName.ToLower().Contains(loweredSearchText)
                        || person.Email != null &&
                            person.Email.ToLower().Contains(loweredSearchText));
                        
                }
            }

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
            {
                query = query.Where(person => person.FirstName.Contains(filter.FirstName.ToLowerInvariant()));
            }

            if (!string.IsNullOrWhiteSpace(filter.LastName))
            {
                query = query.Where(person => person.LastName.Contains(filter.LastName.ToLowerInvariant()));
            }

            if (filter.UseEmailFilter)
            {
                var loweredEmail = filter.Email?.ToLowerInvariant();
                query = query.Where(person => (loweredEmail == null && person.Email == null)
                                              || (person.Email != null
                                                  && loweredEmail != null
                                                  && person.Email.ToLower().Contains(loweredEmail)));
            }

            


            return query;
        }

    }
}
