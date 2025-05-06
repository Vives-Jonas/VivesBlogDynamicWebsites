using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivesBlog.Model;
using VivesBlog.Repository;

namespace VivesBlog.Services
{
    public class PersonService
    {
        private readonly BlogPostDbContext _dbContext;

        public PersonService(BlogPostDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Person> Find()
        {
          return _dbContext.People.ToList();
        }

        public Person? Get(int id)
        {
            return _dbContext.People.FirstOrDefault(p => p.Id == id);
        }

        public Person? Create(Person person)
        {
            _dbContext.People.Add(person);
            _dbContext.SaveChanges();
            return person;
        }

        public Person? Update(int id, Person person)
        {
            var dbPerson = Get(id);
            if (dbPerson == null)
            {
                return null;
            }

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;

            _dbContext.SaveChanges();

            return dbPerson;
        }

        public void Delete(int id)
        {
            var person = Get(id);
            if (person is null)
            {
                return;
            }
            _dbContext.People.Remove(person);
            _dbContext.SaveChanges();
        }
    }
}
