using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Models;
using Business;
using TrainingTrackerAPI.Controllers;
using TrainingTrackerAPITests.Helpers;
using System.Web.Http.Results;
using TrainingTrackerAPI.Models;
using System.Collections.Generic;
using System.Linq;

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

        [TestMethod]
        public void GetAllLogsByIdExpectAllLogsReturnedAnd200Ok()
        {
            var dataLogs = TestHelper.GetTestDataLogList();

            _mockLogBusiness.Setup(x => x.GetAllLogsByUserId(It.IsAny<int>())).Returns(dataLogs);

            var result = _logController.GetLogsByUserId(1);

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<IList<ApiLog>>));

            var apiLogs = (result as OkNegotiatedContentResult<IList<ApiLog>>).Content;

            Assert.AreEqual(dataLogs.FirstOrDefault().DateAdded, apiLogs.FirstOrDefault().DateAdded);
        }

        [TestMethod]
        public void GetLogByIdExpectLogReturnedAnd200Ok()
        {
            var dataLog = TestHelper.GetTestDataLog();

            _mockLogBusiness.Setup(x => x.GetLogById(It.IsAny<int>())).Returns(dataLog);

            var result = _logController.GetLogById(1);

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<ApiLog>));

            var apiLog = (result as OkNegotiatedContentResult<ApiLog>).Content;

            Assert.AreEqual(dataLog.Title, apiLog.Title);
        }
    }
}
