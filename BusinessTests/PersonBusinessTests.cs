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
        [TestMethod]
        public void AddValidPersonExpectPersonAddedAndNoErrorThrown()
        {
            var mockPersonRepository = new Mock<IPersonRepository>();
            mockPersonRepository.Setup(x => x.AddPerson(It.IsAny<Person>())).Verifiable();

            var personBusiness = new PersonBusiness(mockPersonRepository.Object);

            var person = GetTestPerson("testuser");

            personBusiness.AddPersonToDatabase(person);
        }

        [TestMethod]
        public void AddNullPersonExpectPersonNotAdded()
        {
            var mockPersonRepository = new Mock<IPersonRepository>();
            mockPersonRepository.Setup(x => x.AddPerson(It.IsAny<Person>())).Verifiable();

            var personBusiness = new PersonBusiness(mockPersonRepository.Object);

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

            var mockPersonRepository = new Mock<IPersonRepository>();
            mockPersonRepository.Setup(x => x.GetPersonByUsername(It.IsAny<string>())).Returns(GetTestPerson(username));

            var personBusiness = new PersonBusiness(mockPersonRepository.Object);

            var person = personBusiness.GetPersonByUsername(username);

            Assert.IsNotNull(person);
            Assert.AreEqual("Test", person.FirstName);
        }
    }
}
