using Microsoft.EntityFrameworkCore;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;
using VivesBlog.Dto.Requests;
using VivesBlog.Dto.Responses;
using VivesBlog.Model;
using VivesBlog.Repository;
using VivesBlog.Services.Extensions;

namespace VivesBlog.Services
{
    public class PersonService(VivesBlogDbContext dbContext)
    {

        public async Task<IList<PersonResponse>> Find()
        {
          return await dbContext.People
              .AsNoTracking()
              .ProjectToResponse()
              .ToListAsync();

        }

        public async Task<PersonResponse?> Get(int id)
        {
            return await dbContext.People
                .AsNoTracking()
                .ProjectToResponse()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ServiceResult<PersonResponse>> Create(PersonRequest request)
        {
            var serviceResult = new ServiceResult<PersonResponse>();
            if (string.IsNullOrWhiteSpace(request.FirstName))
            {
                serviceResult.Required(nameof(request.FirstName));
            }
            if (string.IsNullOrWhiteSpace(request.LastName))
            {
                serviceResult.Required(nameof(request.FirstName));
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
            return new ServiceResult<PersonResponse>(personResponse);
        }

        public async Task<ServiceResult<PersonResponse>> Update(int id, PersonRequest request)
        {
            var person = await dbContext.People
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
            {
                return new ServiceResult<PersonResponse>().NotFound(nameof(person));
            }

            var serviceResult = new ServiceResult<PersonResponse>();
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
            return new ServiceResult<PersonResponse>(personResponse);
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
