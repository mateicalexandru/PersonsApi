using PersonsApi.Entities;
using PersonsApi.Repositories;

namespace PersonsApi.Services
{
    public interface IPersonService
    {
        Person GetEmployee(int id);

        List<Person> GetEmployees();
        void CreatePerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(int id);
    }
}