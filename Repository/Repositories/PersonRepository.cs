using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IPersonRepository
    {
        void AddPerson(Person person);
        Person GetPersonByUsername(string username);
    }

    public class PersonRepository : BaseSqlRepository, IPersonRepository
    {
        public PersonRepository(IConnectionStringProvider connectionStringProvider)
            : base(connectionStringProvider)
        {
        }

        public void AddPerson(Person person)
        {
            var command = GetCommand("AddPerson", System.Data.CommandType.StoredProcedure);

            AddParameter(command, "@Username", person.Username);
            AddParameter(command, "@Password", person.Password);
            AddParameter(command, "@FirstName", person.FirstName);
            AddParameter(command, "@LastName", person.LastName);
            AddParameter(command, "@Email", person.Email);
            AddParameter(command, "@DOB", person.DOB);
            AddParameter(command, "@Gender", (int)person.Gender);

            ExecuteNonQueryChecked(command);
        }

        public Person GetPersonByUsername(string username)
        {
            var query = @"SELECT * FROM [Person] WHERE Username = @Username";
            var command = GetCommand(query, System.Data.CommandType.Text);

            AddParameter(command, "@Username", username);

            return GetEntitiesFromDatabase<Person>(command).FirstOrDefault();
        }

        protected override object MapRowToEntity(IDataReader reader)
        {
            return new Person()
            {
                Username = reader.GetString(reader.GetOrdinal("Username")),
                Password = reader.GetString(reader.GetOrdinal("Password")),
                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                DOB = reader.GetDateTime(reader.GetOrdinal("DOB")),
                Gender = (Gender)reader.GetInt32(reader.GetOrdinal("GenderId"))
            };
        }
    }
}
