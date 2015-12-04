using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Repository;
using Exceptions;

namespace RepositoryTests
{
    [TestClass]
    public class PersonRepositoryTests
    {
        private IntegrationTestData dataHelper;

        private IPersonRepository _personRepository;

        [TestInitialize]
        public void SetUp()
        {
            _personRepository = new PersonRepository();

            dataHelper = new IntegrationTestData();
            dataHelper.SetUp();
        }

        [TestCleanup]
        public void TearDown()
        {
            dataHelper.CleanUp();
        }

        [TestMethod]
        public void AddPersonToPersonTableExpectPersonAdded()
        {
            var person = CreateTestPerson("testperson1000");

            _personRepository.AddPerson(person);
        }

        [TestMethod]
        [ExpectedException(typeof(RepositoryException), "The email address 'test@user.com' is already in use.")]
        public void AddDuplicatePersonToPersonTableExpectErrorThrown()
        {
            var person = CreateTestPerson("testperson1000");

            _personRepository.AddPerson(person);
            _personRepository.AddPerson(person);
        }

        private Person CreateTestPerson(string username)
        {
            return new Person()
            {
                Username = username,
                Password = "Password1",
                FirstName = "Test",
                LastName = "User",
                Email = "test@user.com",
                DOB = new DateTime(1993, 1, 22),
                Gender = Gender.MALE
            };
        }

        [TestMethod]
        public void GetPersonFromDatabaseByUsername()
        {
            var username = "testperson1000";
            var person = CreateTestPerson(username);

            _personRepository.AddPerson(person);

            var personFromDb = _personRepository.GetPersonByUsername(username);

            Assert.IsNotNull(personFromDb);
            Assert.AreEqual("Test", personFromDb.FirstName);
        }


        [TestMethod]
        public void GetPersonFromDatabaseWhoDoesNotExist()
        {
            var username = "testperson1000";
            var person = CreateTestPerson(username);

            var personFromDb = _personRepository.GetPersonByUsername(username);

            Assert.IsNull(personFromDb);
        }
    }
}
