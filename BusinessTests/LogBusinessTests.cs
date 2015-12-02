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

            var log = GetTestLog(1, "testlog");
        }

        [TestMethod]
        public void GetAllLogsByUsernameFromDatabase()
        {
            _mockLogRepository.Setup(x => x.GetAllLogsByUserId(It.IsAny<int>())).Returns(GetListOfLogs());

            var personBusiness = new LogBusiness(_mockLogRepository.Object);

            var userId = 1;

            var logs = personBusiness.GetAllLogsByUserId(userId);

            Assert.AreEqual("test title", logs[0].Title);
        }

        [TestMethod]
        public void GetAllLogsByUserId0ExpectNoErrorThrown()
        {
            _mockLogRepository.Setup(x => x.GetAllLogsByUserId(It.IsAny<int>())).Returns(GetListOfLogs());

            var personBusiness = new LogBusiness(_mockLogRepository.Object);

            var logs = personBusiness.GetAllLogsByUserId(0);

            Assert.IsNull(logs);
        }

        [TestMethod]
        public void AddLogExpectNoError()
        {
            var personBusiness = new LogBusiness(_mockLogRepository.Object);

            personBusiness.AddLogToDatabase(GetTestLog(1, "testlog"));
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
