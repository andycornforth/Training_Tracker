using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Moq;
using Models;
using Business;
using System.Collections.Generic;
using Exceptions;

namespace BusinessTests
{
    [TestClass]
    public class ExerciseBusinessTests
    {
        private Mock<IExerciseRepository> _mockExerciseRepository;
        private ExerciseBusiness _exersiceBusiness;

        [TestInitialize]
        public void SetUp()
        {
            _mockExerciseRepository = new Mock<IExerciseRepository>();
            _exersiceBusiness = new ExerciseBusiness(_mockExerciseRepository.Object);
        }

        private IList<Exercise> _exercises = new List<Exercise>()
        {
            new Exercise() { Id = 1, Title = "Squat"},
            new Exercise() { Id = 2, Title = "Deadlift" }
        };

        private Exercise _exercise = new Exercise() { Id = 1, Title = "Squat" };

        [TestMethod]
        public void GetAllExercieseReturnsAllExercises()
        {
            _mockExerciseRepository.Setup(x => x.GetAllExercises()).Returns(_exercises);

            var exercises = _exersiceBusiness.GetAllExercises();

            Assert.AreEqual(2, exercises.Count);
        }

        [TestMethod]
        public void AddExerciseExpectExerciseReturned()
        {
            _mockExerciseRepository.Setup(x => x.AddExercise(It.IsAny<string>())).Returns(_exercise);

            var exercise = _exersiceBusiness.GetExercise("Squat");

            Assert.IsNotNull(exercise);
            Assert.AreEqual(1, exercise.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void AddExerciseWithNoNameExpectErrorThrown()
        {
            _mockExerciseRepository.Setup(x => x.AddExercise(It.IsAny<string>())).Returns(_exercise);

            var exercise = _exersiceBusiness.GetExercise("");
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void AddExerciseWithNullNameExpectErrorThrown()
        {
            _mockExerciseRepository.Setup(x => x.AddExercise(It.IsAny<string>())).Returns(_exercise);

            var exercise = _exersiceBusiness.GetExercise(null);
        }

        [TestMethod]
        public void GetExerciseByIdDoesNotThrowError()
        {
            _mockExerciseRepository.Setup(x => x.GetExerciseById(It.IsAny<int>())).Returns(_exercise);

            var exercise = _exersiceBusiness.GetExerciseById(1);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void GetExerciseByIdThrowsErrorWhenNullIsReturnedFromRepository()
        {
            _mockExerciseRepository.Setup(x => x.GetExerciseById(It.IsAny<int>())).Returns((Exercise)null);

            var exercise = _exersiceBusiness.GetExerciseById(1);
        }
    }
}
