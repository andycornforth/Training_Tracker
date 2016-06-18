﻿using System;
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
        [ExpectedException(typeof(RepositoryException))]
        public void Add2SetsToLogWithTheSamePositionInLogsExpectRepositoryException()
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
            _setRepository.AddSet(set);
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
        public void EditSetExpectSetEdited()
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

            Assert.AreEqual(82.5, set.Weight);

            _exerciseRepository.AddExercise("Overhead Press");
            var newExercise = _exerciseRepository.GetAllExercises().Where(x => x.Id != exercise.Id).FirstOrDefault();

            set = new Set()
            {
                Id = set.Id,
                Exercise = new Exercise() { Id = newExercise.Id },
                Weight = 60,
                Reps = 10,
                PositionInLog = 2
            };

            _setRepository.UpdateSet(set);

            set = _setRepository.GetSetsByLogId(log.Id).FirstOrDefault();

            Assert.AreEqual(newExercise.Id, set.Exercise.Id);
            Assert.AreEqual(60, set.Weight);
            Assert.AreEqual(10, set.Reps);
            Assert.AreEqual(2, set.PositionInLog);
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
