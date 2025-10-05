using Microsoft.EntityFrameworkCore;
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
          return await dbContext.People.AsNoTracking()
              .Include(p => p.Articles)
              .ProjectToResponse()
              .ToListAsync();

        }

        public async Task<PersonResponse?> Get(int id)
        {
            return await dbContext.People.AsNoTracking()
                .Include(p => p.Articles)
                .ProjectToResponse()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PersonResponse?> Create(PersonRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName))
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(request.LastName))
            {
                return null;
            }

            var person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
            };

            dbContext.People.Add(person);
            await dbContext.SaveChangesAsync();

            return await Get(person.Id);
        }

        public async Task<PersonResponse?> Update(int id, PersonRequest request)
        {
            var person = await dbContext.People
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
            {
                return null;
            }

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.Email = request.Email;

            await dbContext.SaveChangesAsync();

            return await Get(person.Id);
        }

        public async Task Delete(int id)
        {
            var person = await dbContext.People
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person is null)
            {
                return;
            }
            dbContext.People.Remove(person);
            await dbContext.SaveChangesAsync();
        }
    }
}
