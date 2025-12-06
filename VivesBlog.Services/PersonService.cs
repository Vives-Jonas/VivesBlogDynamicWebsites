using Microsoft.EntityFrameworkCore;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;
using VivesBlog.Dto.Filter;
using VivesBlog.Dto.Requests;
using VivesBlog.Dto.Results;
using VivesBlog.Model;
using VivesBlog.Repository;
using VivesBlog.Services.Extensions;
using VivesBlog.Services.Extensions.Filters;

namespace VivesBlog.Services
{
    public class PersonService(VivesBlogDbContext dbContext)
    {

        public async Task<FilteredPagedServiceResult<PersonResult, PersonFilter>> Find(Paging paging, string? sorting, PersonFilter? filter)
        {
            var query = dbContext.People
                .AsNoTracking()
                .ApplyFilter(filter);
            var totalCount = await query.CountAsync();

            sorting ??= $"{nameof(PersonResult.LastName)}, {nameof(PersonResult.FirstName)}";

            var people = await query                
                .OrderBy(sorting)
                .ApplyPaging(paging)
                .ProjectToResult()
                .ToListAsync();

            return new FilteredPagedServiceResult<PersonResult, PersonFilter>
            {
                Data = people,
                TotalCount = totalCount,
                Paging = paging,
                Sorting = sorting,
                Filter = filter
            };
        }

        public async Task<PersonResult?> Get(int id)
        {
            return await dbContext.People
                .AsNoTracking()
                .ProjectToResult()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ServiceResult<PersonResult>> Create(PersonRequest request)
        {
            var serviceResult = new ServiceResult<PersonResult>();
            if (string.IsNullOrWhiteSpace(request.FirstName))
            {
                serviceResult.Required(nameof(request.FirstName));
            }
            if (string.IsNullOrWhiteSpace(request.LastName))
            {
                serviceResult.Required(nameof(request.FirstName));
            }
            if (!serviceResult.IsSuccess)
            {
                return serviceResult;
            }

            var person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
            };

            dbContext.People.Add(person);
            await dbContext.SaveChangesAsync();

            var personResponse = await Get(person.Id);
            return new ServiceResult<PersonResult>(personResponse);
        }

        public async Task<ServiceResult<PersonResult>> Update(int id, PersonRequest request)
        {
            var person = await dbContext.People
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
            {
                return new ServiceResult<PersonResult>().NotFound(nameof(person));
            }

            var serviceResult = new ServiceResult<PersonResult>();
            if (string.IsNullOrWhiteSpace(request.FirstName))
            {
                serviceResult.Required(nameof(request.FirstName));
            }
            if (string.IsNullOrWhiteSpace(request.LastName))
            {
                serviceResult.Required(nameof(request.FirstName));
            }

            if (!serviceResult.IsSuccess)
            {
                return serviceResult;
            }

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.Email = request.Email;

            await dbContext.SaveChangesAsync();

            var personResponse = await Get(id);
            return new ServiceResult<PersonResult>(personResponse);
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var person = await dbContext.People
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person is null)
            {
                return new ServiceResult().AlreadyRemoved();
            }
            dbContext.People.Remove(person);
            await dbContext.SaveChangesAsync();
            return new ServiceResult();
        }
    }
}
