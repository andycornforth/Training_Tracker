using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Models;
using Business;
using TrainingTrackerAPI.Controllers;

namespace TrainingTrackerAPITests
{
    [TestClass]
    public class PersonControllerTests
    {
        [TestMethod]
        public void AddPersonExpectTrue()
        {
            var mockPersonBusiness = new Mock<IPersonBusiness>();
            mockPersonBusiness.Setup(x => x.AddPersonToDatabase(It.IsAny<Person>()));

            var personController = new PersonController(mockPersonBusiness.Object);

            var person = new Person()
            {
                Username = "testuser",
                Password = "Password1",
                FirstName = "Test",
                LastName = "User",
                Email = "test@user.com",
                DOB = new DateTime(1993, 1, 22),
                Gender = Gender.MALE
            };

            var result = personController.AddPerson(person);

            Assert.IsTrue(result is System.Web.Http.Results.OkResult);
        }

        //[TestMethod]
        //public void AddNullPersonExpectFalse()
        //{
        //    var mockPersonBusiness = new Mock<IPersonBusiness>();
        //    mockPersonBusiness.Setup(x => x.AddPersonToDatabase(It.IsAny<Person>()));

        //    var personController = new PersonController(mockPersonBusiness.Object);

        //    var result = personController.AddPerson(null);

        //    Assert.IsTrue(result is System.Web.Http.Results.BadRequestResult);
        //}
    }
}
