using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Repository;
using System.Data.SqlClient;

namespace RepositoryTests
{
    [TestClass]
    public class LogRepositoryTests
    {
        private IntegrationTestData dataHelper;

        private ILogRepository _logRepository;
        private IPersonRepository _personRepository;

        [TestInitialize]
        public void SetUp()
        {
            _logRepository = new LogRepository();
            _personRepository = new PersonRepository();

            dataHelper = new IntegrationTestData();
            dataHelper.SetUp();
        }

        [TestCleanup]
        public void TearDown()
        {
            dataHelper.CleanUp();
        }

        [TestMethod]
        public void AddLogToLogTableExpectLogAdded()
        {
            var username = "testuser";
            var logTitle = "newlog";

            AddPersonToDatabase(username);

            _logRepository.AddLog(CreateTestLog(username, logTitle));
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void AUserCanNotHave2LogsWithTheSameName()
        {
            var username = "testuser";
            var logTitle = "newlog";

            AddPersonToDatabase(username);

            _logRepository.AddLog(CreateTestLog(username, logTitle));
            _logRepository.AddLog(CreateTestLog(username, logTitle));
        }

        [TestMethod]
        public void _2UsersCanHaveALogWithTheSameName()
        {
            var username = "testuser";
            var username2 = "testuser2";
            var logTitle = "newlog";

            AddPersonToDatabase(username);
            AddPersonToDatabase(username2);

            _logRepository.AddLog(CreateTestLog(username, logTitle));
            _logRepository.AddLog(CreateTestLog(username2, logTitle));
        }

        [TestMethod]
        public void GetAllLogsFromDatabaseByUsername()
        {
            var username = "testuser";
            var logTitle = "newlog";
            var logTitle2 = "secondLog";

            AddPersonToDatabase(username);

            _logRepository.AddLog(CreateTestLog(username, logTitle));
            _logRepository.AddLog(CreateTestLog(username, logTitle2));

            var allLogs = _logRepository.GetAllLogsByUsername(username);

            Assert.AreEqual(2, allLogs.Count);
            Assert.AreEqual("newlog", allLogs[0].Title);
        }

        [TestMethod]
        public void GetLogFromDatabaseById()
        {
            var username = "testuser";
            var logTitle = "newlog";

            AddPersonToDatabase(username);

            _logRepository.AddLog(CreateTestLog(username, logTitle));
            var logs = _logRepository.GetAllLogsByUsername(username);

            var log = _logRepository.GetLogById(logs[0].Id);

            Assert.AreEqual(logs[0].Id, log.Id);
            Assert.AreEqual("testuser", log.Username);
            Assert.AreEqual("newlog", log.Title);
        }

        private Log CreateTestLog(string username, string logTitle)
        {
            return new Log()
            {
                Username = username,
                Title = logTitle
            };
        }

        private void AddPersonToDatabase(string username)
        {
            var person = new Person()
            {
                Username = username,
                Password = "Password1",
                FirstName = "Test",
                LastName = "User",
                Email = "test@user.com",
                DOB = new DateTime(1993, 1, 22),
                Gender = Gender.MALE
            };

            _personRepository.AddPerson(person);
        }
    }
}
