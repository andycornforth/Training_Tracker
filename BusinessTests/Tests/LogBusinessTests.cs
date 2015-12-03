using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Moq;
using Models;
using Business;
using System.Collections.Generic;
using Business.Exceptions;

namespace BusinessTests
{
    [TestClass]
    public class LogBusinessTests
    {
        private Mock<ILogRepository> _mockLogRepository;
        private ILogBusiness _logBusiness;

        [TestInitialize]
        public void SetUp()
        {
            _mockLogRepository = new Mock<ILogRepository>();
        }

        private void InitializeLogBusiness()
        {
            _logBusiness = new LogBusiness(_mockLogRepository.Object);
        }

        [TestMethod]
        public void GetAllLogsByUsernameFromDatabase()
        {
            _mockLogRepository.Setup(x => x.GetAllLogsByUserId(It.IsAny<int>())).Returns(GetListOfLogs());

            InitializeLogBusiness();

            var userId = 1;

            var logs = _logBusiness.GetAllLogsByUserId(userId);

            Assert.AreEqual("test title", logs[0].Title);
        }

        [TestMethod]
        public void GetAllLogsByUserId0ExpectNoErrorThrown()
        {
            _mockLogRepository.Setup(x => x.GetAllLogsByUserId(It.IsAny<int>())).Returns(GetListOfLogs());

            InitializeLogBusiness();

            var logs = _logBusiness.GetAllLogsByUserId(0);

            Assert.IsNull(logs);
        }

        [TestMethod]
        public void AddLogExpectNoError()
        {
            InitializeLogBusiness();

            _logBusiness.AddLogToDatabase(GetTestLog(1, "testlog"));
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void AddNullLogExpectErrorThrown()
        {
            InitializeLogBusiness();

            _logBusiness.AddLogToDatabase(null);
        }

        private IList<Log> GetListOfLogs()
        {
            return new List<Log>()
            {
                new Log()
                {
                    Id=1,
                    PersonId = 1,
                    Title ="test title"
                }
            };
        }

        private Log GetTestLog(int userId, string title)
        {
            return new Log()
            {
                PersonId = userId,
                Title = title
            };
        }
    }
}
