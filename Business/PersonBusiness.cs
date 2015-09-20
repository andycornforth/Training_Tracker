using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IPersonBusiness
    {
        void AddPersonToDatabase(Person person);
        Person GetPersonByUsername(string username);
    }

    public class PersonBusiness : IPersonBusiness
    {
        private IPersonRepository _personRepository;

        public PersonBusiness()
        {
            _personRepository = new PersonRepository(new ConnectionStringProvider());
        }

        public PersonBusiness(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public void AddPersonToDatabase(Person person)
        {
            if (person != null)
            {
                person.Password = Encryption.HashWithSalt(person.Password);
                _personRepository.AddPerson(person);
            }
        }

        public Person GetPersonByUsername(string username)
        {
            return _personRepository.GetPersonByUsername(username);
        }
    }
}
