using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Moq;
using Models;
using Business;

namespace BusinessTests
{
    [TestClass]
    public class PersonBusinessTests
    {
        private Mock<IPersonRepository> _mockPersonRepository;

        [TestInitialize]
        public void SetUp()
        {
            _mockPersonRepository = new Mock<IPersonRepository>();
        }

        [TestMethod]
        public void AddValidPersonExpectPersonAddedAndNoErrorThrown()
        {
            _mockPersonRepository.Setup(x => x.AddPerson(It.IsAny<Person>())).Verifiable();

            var personBusiness = new PersonBusiness(_mockPersonRepository.Object);

            var person = GetTestPerson("testuser");

            personBusiness.AddPersonToDatabase(person);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Date of birth cannot be in the future")]
        public void AddPersonWithDateOfBirthInTheFutureExpectErrorThrown()
        {
            var personBusiness = new PersonBusiness(_mockPersonRepository.Object);

            var person = GetTestPerson("testuser");
            person.DOB = new DateTime(2100, 1, 1);

            personBusiness.AddPersonToDatabase(person);
        }

        [TestMethod]
        public void AddNullPersonExpectPersonNotAdded()
        {
            _mockPersonRepository.Setup(x => x.AddPerson(It.IsAny<Person>())).Verifiable();

            var personBusiness = new PersonBusiness(_mockPersonRepository.Object);

            personBusiness.AddPersonToDatabase(null);
        }

        private Person GetTestPerson(string username)
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
        public void GetPersonByUsernameExpectPerson()
        {
            var username = "test123";

            _mockPersonRepository.Setup(x => x.GetPersonByUsername(It.IsAny<string>())).Returns(GetTestPerson(username));

            var personBusiness = new PersonBusiness(_mockPersonRepository.Object);

            var person = personBusiness.GetPersonByUsername(username);

            Assert.IsNotNull(person);
            Assert.AreEqual("Test", person.FirstName);
        }
    }
}
