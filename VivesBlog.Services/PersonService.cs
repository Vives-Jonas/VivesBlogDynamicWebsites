using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VivesBlog.Model;
using VivesBlog.Repository;

namespace VivesBlog.Services
{
    public class PersonService(VivesBlogDbContext dbContext)
    {

        public async Task<IList<Person>> Find()
        {
          return await dbContext.People.ToListAsync();
        }

        public async Task<Person?> Get(int id)
        {
            return await dbContext.People.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Person?> Create(Person person)
        {
            dbContext.People.Add(person);
            await dbContext.SaveChangesAsync();
            return person;
        }

        public async Task<Person?> Update(int id, Person person)
        {
            var dbPerson = await Get(id);
            if (dbPerson == null)
            {
                return null;
            }

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;

            await dbContext.SaveChangesAsync();

            return dbPerson;
        }

        public async Task Delete(int id)
        {
            var person = await Get(id);
            if (person is null)
            {
                return;
            }
            dbContext.People.Remove(person);
            await dbContext.SaveChangesAsync();
        }
    }
}
