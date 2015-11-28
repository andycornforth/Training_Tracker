using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Moq;
using Models;
using Business;
using System.Collections.Generic;

namespace BusinessTests
{
    [TestClass]
    public class LogBusinessTests
    {
        private Mock<ILogRepository> _mockLogRepository;

        [TestInitialize]
        public void SetUp()
        {
            _mockLogRepository = new Mock<ILogRepository>();
        }

        [TestMethod]
        public void AddValidPersonExpectPersonAddedAndNoErrorThrown()
        {
            _mockLogRepository.Setup(x => x.AddLog(It.IsAny<Log>())).Verifiable();

            var personBusiness = new LogBusiness(_mockLogRepository.Object);

            var log = GetTestLog("testuser", "testlog");
        }

        [TestMethod]
        public void GetAllLogsByUsernameFromDatabase()
        {
            _mockLogRepository.Setup(x => x.GetAllLogsByUsername(It.IsAny<string>())).Returns(GetListOfLogs());

            var personBusiness = new LogBusiness(_mockLogRepository.Object);

            var username = "testuser";

            var logs = personBusiness.GetAllLogsByUsername(username);

            Assert.AreEqual("test title", logs[0].Title);
        }

        [TestMethod]
        public void GetAllLogsByUsernameWithNullExpectNoErrorThrown()
        {
            _mockLogRepository.Setup(x => x.GetAllLogsByUsername(It.IsAny<string>())).Returns(GetListOfLogs());

            var personBusiness = new LogBusiness(_mockLogRepository.Object);

            var logs = personBusiness.GetAllLogsByUsername(null);

            Assert.IsNull(logs);
        }

        [TestMethod]
        public void AddLogExpectNoError()
        {
            var personBusiness = new LogBusiness(_mockLogRepository.Object);

            personBusiness.AddLogToDatabase(GetTestLog("testuser", "testlog"));
        }

        [TestMethod]
        public void AddNullLogExpectNoError()
        {
            var personBusiness = new LogBusiness(_mockLogRepository.Object);

            personBusiness.AddLogToDatabase(null);
        }

        private IList<Log> GetListOfLogs()
        {
            return new List<Log>()
            {
                new Log()
                {
                    Id=1,
                    Username ="testuser",
                    Title ="test title"
                }
            };
        }

        private Log GetTestLog(string username, string title)
        {
            return new Log()
            {
                Username = username,
                Title = title
            };
        }
    }
}
