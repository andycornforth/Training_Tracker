using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Moq;
using Models;
using Business;
using Exceptions;

namespace BusinessTests
{
    [TestClass]
    public class PersonBusinessTests
    {
        private Mock<IPersonRepository> _mockPersonRepository;
        private IPersonBusiness _personBusiness;

        [TestInitialize]
        public void SetUp()
        {
            _mockPersonRepository = new Mock<IPersonRepository>();
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

        private void InitializePersonBusiness()
        {
            _personBusiness = new PersonBusiness(_mockPersonRepository.Object);
        }

        [TestMethod]
        public void AddValidPersonExpectPersonAddedAndNoErrorThrown()
        {
            _mockPersonRepository.Setup(x => x.AddPerson(It.IsAny<Person>())).Verifiable();

            InitializePersonBusiness();

             var person = GetTestPerson("testuser");

            _personBusiness.AddPersonToDatabase(person);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Date of birth cannot be in the future")]
        public void AddPersonWithDateOfBirthInTheFutureExpectErrorThrown()
        {
            InitializePersonBusiness();

            var person = GetTestPerson("testuser");
            person.DOB = new DateTime(2100, 1, 1);

            _personBusiness.AddPersonToDatabase(person);
        }

        [TestMethod]
        public void AddNullPersonExpectPersonNotAdded()
        {
            _mockPersonRepository.Setup(x => x.AddPerson(It.IsAny<Person>())).Verifiable();

            InitializePersonBusiness();

            _personBusiness.AddPersonToDatabase(null);
        }

        [TestMethod]
        public void GetPersonByUsernameExpectPerson()
        {
            var username = "test123";

            _mockPersonRepository.Setup(x => x.GetPersonByUsername(It.IsAny<string>())).Returns(GetTestPerson(username));

            InitializePersonBusiness();

            var person = _personBusiness.GetPersonByUsername(username);

            Assert.IsNotNull(person);
            Assert.AreEqual("Test", person.FirstName);
        }
    }
}
