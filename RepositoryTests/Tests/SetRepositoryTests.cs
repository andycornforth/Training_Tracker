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
    public class SetRepositoryTests
    {
        private IntegrationTestData dataHelper;
        private ISetRepository _setRepository;
        private ILogRepository _logRepository;
        private IPersonRepository _personRepository;
        private IExerciseRepository _exerciseRepository;

        [TestInitialize]
        public void SetUp()
        {
            _setRepository = new SetRepository();
            _logRepository = new LogRepository();
            _personRepository = new PersonRepository();
            _exerciseRepository = new ExerciseRepository();

            dataHelper = new IntegrationTestData();
            dataHelper.SetUp();
        }

        [TestCleanup]
        public void TearDown()
        {
            dataHelper.CleanUp();
        }

        private Person CreateTestPerson(string username)
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
        public void AddSetExpectSetAdded()
        {
            Log log;
            Exercise exercise;
            CreateTestLogAndExercise(out log, out exercise);

            var set = new Set()
            {
                Exercise = exercise,
                Log = log,
                Weight = 82.5,
                Reps = 5,
                PositionInLog = 1
            };
            _setRepository.AddSet(set);

            set = _setRepository.GetSetsByLogId(log.Id).FirstOrDefault();

            Assert.IsNotNull(set);
            Assert.AreNotEqual(0, set.Id);
            Assert.AreEqual(log.Id, set.Log.Id);
            Assert.AreEqual(log.PersonId, set.Log.PersonId);
            Assert.AreEqual(exercise.Id, set.Exercise.Id);
            Assert.AreEqual(exercise.Title, set.Exercise.Title);
            Assert.AreEqual(82.5, set.Weight);
            Assert.AreEqual(5, set.Reps);
            Assert.AreEqual(1, set.PositionInLog);
        }

        [TestMethod]
        public void Add2SetsToLogWithDifferentPositionInLogsExpectSetsAdded()
        {
            Log log;
            Exercise exercise;
            CreateTestLogAndExercise(out log, out exercise);

            var set = new Set()
            {
                Exercise = exercise,
                Log = log,
                Weight = 82.5,
                Reps = 5,
                PositionInLog = 1
            };
            _setRepository.AddSet(set);

            set.PositionInLog = 2;
            _setRepository.AddSet(set);

            var sets = _setRepository.GetSetsByLogId(log.Id);

            Assert.AreEqual(2, sets.Count);
        }

        [TestMethod]
        public void Add2SetsToLogWithTheSamePositionExpectSetToOverwrite()
        {
            Log log;
            Exercise exercise;
            CreateTestLogAndExercise(out log, out exercise);

            var set = new Set()
            {
                Exercise = exercise,
                Log = log,
                Weight = 82.5,
                Reps = 5,
                PositionInLog = 1
            };
            _setRepository.AddSet(set);

            var newSet = new Set()
            {
                Exercise = exercise,
                Log = log,
                Weight = 82.5,
                Reps = 6,
                PositionInLog = 1
            };
            _setRepository.AddSet(newSet);

            var returnedSets = _setRepository.GetSetsByLogId(log.Id);

            Assert.AreEqual(1, returnedSets.Count);
            Assert.AreEqual(6, returnedSets.First().Reps);
        }


        [TestMethod]
        public void GetLatestSetFromLogExpectCorrectDataReturned()
        {
            Log log;
            Exercise exercise;
            CreateTestLogAndExercise(out log, out exercise);

            var set = new Set()
            {
                Exercise = exercise,
                Log = log,
                Weight = 82.5,
                Reps = 5,
                PositionInLog = 1
            };
            _setRepository.AddSet(set);

            set.PositionInLog = 2;
            _setRepository.AddSet(set);

            var userId = _personRepository.GetPersonByUsername("andycornforth").Id;
            var logId = _logRepository.GetAllLogsByUserId(userId).FirstOrDefault().Id;

            var newSet = _setRepository.GetLatestSetForLog(logId);

            Assert.AreEqual(2, newSet.PositionInLog);
        }

        [TestMethod]
        public void GetLatestSetFromLogWhenNoneAddedExpectNullReturned()
        {
            Log log;
            Exercise exercise;
            CreateTestLogAndExercise(out log, out exercise);

            var set = new Set()
            {
                Exercise = exercise,
                Log = log,
                Weight = 82.5,
                Reps = 5,
                PositionInLog = 1
            };

            var userId = _personRepository.GetPersonByUsername("andycornforth").Id;
            var logId = _logRepository.GetAllLogsByUserId(userId).FirstOrDefault().Id;

            var newSet = _setRepository.GetLatestSetForLog(logId);

            Assert.IsNull(newSet);
        }

        [TestMethod]
        public void DeleteSetExpectSetDeleted()
        {
            Log log;
            Exercise exercise;
            CreateTestLogAndExercise(out log, out exercise);

            var set = new Set()
            {
                Exercise = exercise,
                Log = log,
                Weight = 82.5,
                Reps = 5,
                PositionInLog = 1
            };
            _setRepository.AddSet(set);

            set = _setRepository.GetSetsByLogId(log.Id).FirstOrDefault();

            _setRepository.DeleteSet(log.Id, set.Id);

            var sets = _setRepository.GetSetsByLogId(log.Id);

            Assert.AreEqual(0, sets.Count);
        }

        [TestMethod]
        public void DeleteSetExpectSetsWithHigherPositionsToBeMovedDownPosition()
        {
            Log log;
            Exercise exercise;
            CreateTestLogAndExercise(out log, out exercise);

            var set = new Set()
            {
                Exercise = exercise,
                Log = log,
                Weight = 82.5,
                Reps = 5,
                PositionInLog = 1
            };
            _setRepository.AddSet(set);
            set.PositionInLog = 2;
            set.Reps = 4; // changed reps to check it is a different set
            _setRepository.AddSet(set);
            set.PositionInLog = 3;
            set.Reps = 3;
            _setRepository.AddSet(set);

            var sets = _setRepository.GetSetsByLogId(log.Id);

            set.Id = sets.FirstOrDefault().Id;

            _setRepository.DeleteSet(log.Id, set.Id);

            sets = _setRepository.GetSetsByLogId(log.Id);

            Assert.AreEqual(2, sets.Count);
            Assert.AreEqual(1, sets.FirstOrDefault().PositionInLog);
            Assert.AreEqual(2, sets.LastOrDefault().PositionInLog);
            Assert.AreEqual(4, sets.FirstOrDefault().Reps);
            Assert.AreEqual(3, sets.LastOrDefault().Reps);
        }

        private void CreateTestLogAndExercise(out Log log, out Exercise exercise)
        {
            var person = CreateTestPerson("andycornforth");
            _personRepository.AddPerson(person);
            person = _personRepository.GetPersonByUsername("andycornforth");

            log = new Log()
            {
                PersonId = person.Id,
                Title = "Test Log"
            };
            _logRepository.AddLog(log);
            log = _logRepository.GetAllLogsByUserId(person.Id).FirstOrDefault();

            _exerciseRepository.AddExercise("Squat");
            exercise = _exerciseRepository.GetAllExercises().FirstOrDefault();
        }
    }
}
