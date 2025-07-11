using PersonsApi.Entities;
using PersonsApi.Repositories;

namespace PersonsApi.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly ICompanyRepository _companyRepository;
        public PersonService(IPersonRepository personRepository, ICompanyRepository companyRepository)
        {
            _personRepository = personRepository;
            _companyRepository = companyRepository;
        }
        public Person GetEmployee(int id)
        {
            return _personRepository.GetPersonById(id);
        }

        public List<Person> GetEmployees()
        {
            return _personRepository.GetPersons();
        }

        public void CreatePerson(Person person)
        {
            _personRepository.CreatePerson(person);
        }

        public void UpdatePerson(Person person)
        {
            _personRepository.UpdatePerson(person);
        }

        public void DeletePerson(int id)
        {
            _personRepository.DeletePerson(id);
        }
    }
}
