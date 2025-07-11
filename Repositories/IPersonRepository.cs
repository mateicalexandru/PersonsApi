using PersonsApi.Entities;

namespace PersonsApi.Repositories
{
    public interface IPersonRepository
    {
        Person GetPersonById(int id);
        List<Person> GetPersons();
        void CreatePerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(int id);
    }
}