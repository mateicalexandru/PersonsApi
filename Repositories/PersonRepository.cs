using PersonsApi.Entities;

namespace PersonsApi.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private static List<Person> _persons = new List<Person>
        {
            new Person { Id = 1, Name = "John Doe 1", Company = new Company { Id = 1, Name = "Company 1" } },
            new Person { Id = 2, Name = "John Doe 2", Company = new Company { Id = 2, Name = "Company 2" } },
            new Person { Id = 3, Name = "John Doe 3", Company = new Company { Id = 3, Name = "Company 3" } }
        };

        public Person GetPersonById(int id)
        {
            return new Person
            {
                Id = id,
                Name = "John Doe",
                Company = new Company { Id = 1, Name = "Company 1"},
            };
        }

        public List<Person> GetPersons()
        {
            return _persons;
        }

        public void CreatePerson(Person person)
        {
            person.Id = _persons.Count > 0 ? _persons.Max(p => p.Id) + 1 : 1;
            _persons.Add(person);
        }

        public void UpdatePerson(Person person)
        {
            var existing = _persons.FirstOrDefault(p => p.Id == person.Id);
            if (existing != null)
            {
                existing.Name = person.Name;
                existing.Company = person.Company;
            }
        }

        public void DeletePerson(int id)
        {
            var person = _persons.FirstOrDefault(p => p.Id == id);
            if (person != null)
            {
                _persons.Remove(person);
            }
        }
    }
}
