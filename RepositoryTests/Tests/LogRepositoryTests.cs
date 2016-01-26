using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Repository;
using System.Data.SqlClient;
using System.Linq;
using Exceptions;

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
            var email = "test@user.com";
            var logTitle = "newlog";

            AddPersonToDatabase(email);
            var userId = _personRepository.GetPersonByUsername(email).Id;

            var id = _logRepository.AddLog(CreateTestLog(userId, logTitle));

            Assert.AreNotEqual(0, id);
        }

        [TestMethod]
        public void AddLogExpectDateToBeAdded()
        {
            var email = "test@user.com";
            var logTitle = "newlog";

            AddPersonToDatabase(email);
            var userId = _personRepository.GetPersonByUsername(email).Id;

            _logRepository.AddLog(CreateTestLog(userId, logTitle));
            var allLogs = _logRepository.GetAllLogsByUserId(userId);

            Assert.AreEqual(DateTime.Today, allLogs.FirstOrDefault().DateAdded.Date);
        }

        [TestMethod]
        [ExpectedException(typeof(RepositoryException))]
        public void AUserCanNotHave2LogsWithTheSameName()
        {
            var email = "test@user.com";
            var logTitle = "newlog";

            AddPersonToDatabase(email);
            var userId = _personRepository.GetPersonByUsername(email).Id;

            _logRepository.AddLog(CreateTestLog(userId, logTitle));
            _logRepository.AddLog(CreateTestLog(userId, logTitle));
        }

        [TestMethod]
        [ExpectedException(typeof(RepositoryException))]
        public void LogWithTitleNullExpectErrorThrown()
        {
            var email = "test@user.com";

            AddPersonToDatabase(email);
            var userId = _personRepository.GetPersonByUsername(email).Id;

            _logRepository.AddLog(CreateTestLog(userId, null));
        }

        [TestMethod]
        [ExpectedException(typeof(RepositoryException))]
        public void LogWithTitleEmptyStringExpectErrorThrown()
        {
            var email = "test@user.com";

            AddPersonToDatabase(email);
            var userId = _personRepository.GetPersonByUsername(email).Id;

            _logRepository.AddLog(CreateTestLog(userId, string.Empty));
        }

        [TestMethod]
        public void _2UsersCanHaveALogWithTheSameName()
        {

            var email = "test@user.com";
            var email2 = "test2@user.com";

            var logTitle = "newlog";

            AddPersonToDatabase(email);
            AddPersonToDatabase(email2);

            var userId = _personRepository.GetPersonByUsername(email).Id;
            var userId2 = _personRepository.GetPersonByUsername(email2).Id;

            _logRepository.AddLog(CreateTestLog(userId, logTitle));
            _logRepository.AddLog(CreateTestLog(userId2, logTitle));
        }

        [TestMethod]
        public void GetAllLogsFromDatabaseByUsername()
        {
            var email = "test@user.com";

            var logTitle = "newlog";
            var logTitle2 = "secondLog";

            AddPersonToDatabase(email);
            var userId = _personRepository.GetPersonByUsername(email).Id;

            _logRepository.AddLog(CreateTestLog(userId, logTitle));
            _logRepository.AddLog(CreateTestLog(userId, logTitle2));

            var allLogs = _logRepository.GetAllLogsByUserId(userId);

            Assert.AreEqual(2, allLogs.Count);
            Assert.AreEqual("newlog", allLogs[0].Title);
        }

        [TestMethod]
        public void GetLogFromDatabaseById()
        {
            var email = "test@user.com";

            var logTitle = "newlog";

            AddPersonToDatabase(email);
            var userId = _personRepository.GetPersonByUsername(email).Id;

            _logRepository.AddLog(CreateTestLog(userId, logTitle));
            var logs = _logRepository.GetAllLogsByUserId(userId);

            var log = _logRepository.GetLogById(logs[0].Id);

            Assert.AreEqual(logs[0].Id, log.Id);
            Assert.AreEqual(userId, log.PersonId);
            Assert.AreEqual("newlog", log.Title);
        }

        private Log CreateTestLog(int userId, string logTitle)
        {
            return new Log()
            {
                PersonId = userId,
                Title = logTitle
            };
        }

        private void AddPersonToDatabase(string email)
        {
            var person = new Person()
            {
                Username = email,
                Password = "Password1",
                FirstName = "Test",
                LastName = "User",
                Email = email,
                DOB = new DateTime(1993, 1, 22),
                Gender = Gender.MALE
            };

            _personRepository.AddPerson(person);
        }
    }
}
