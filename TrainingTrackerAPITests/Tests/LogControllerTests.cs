using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Models;
using Business;
using TrainingTrackerAPI.Controllers;
using TrainingTrackerAPITests.Helpers;
using System.Web.Http.Results;
using TrainingTrackerAPI.Models;

namespace TrainingTrackerAPITests
{
    [TestClass]
    public class LogControllerTests : BaseControllerTests
    {
        [TestInitialize]
        public void Initialize()
        {
            _mockLogBusiness = new Mock<ILogBusiness>();
            _logController = new LogController(_mockLogBusiness.Object);
        }

        [TestMethod]
        public void AddLogExpect200OkAndIdReturned()
        {
            _mockLogBusiness.Setup(x => x.AddLogToDatabase(It.IsAny<Log>())).Returns(5);

            var result = _logController.AddLog(TestHelper.GetTestApiLog());

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<int>));

            var logId = (result as OkNegotiatedContentResult<int>).Content;

            Assert.AreEqual(5, logId);
        }
    }
}
