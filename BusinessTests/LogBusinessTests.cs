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
        [TestMethod]
        public void AddValidPersonExpectPersonAddedAndNoErrorThrown()
        {
            var mockLogRepository = new Mock<ILogRepository>();
            mockLogRepository.Setup(x => x.AddLog(It.IsAny<Log>())).Verifiable();

            var personBusiness = new LogBusiness(mockLogRepository.Object);

            var log = GetTestLog("testuser", "testlog");
        }

        [TestMethod]
        public void GetAllLogsFromDatabase()
        {
            var mockLogRepository = new Mock<ILogRepository>();
            mockLogRepository.Setup(x => x.GetAllLogsByUsername(It.IsAny<string>())).Returns(GetListOfLogs());

            var personBusiness = new LogBusiness(mockLogRepository.Object);

            var username = "testuser";

            var logs = personBusiness.GetAllLogsByUsername(username);

            Assert.AreEqual("test title", logs[0].Title);
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
