using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Repository;

namespace RepositoryTests
{
    [TestClass]
    public class ExerciseRepositoryTests
    {
        private IntegrationTestData dataHelper;

        private IExerciseRepository _exerciseRepository;

        [TestInitialize]
        public void SetUp()
        {
            _exerciseRepository = new ExerciseRepository();

            dataHelper = new IntegrationTestData();
            dataHelper.SetUp();
        }

        [TestCleanup]
        public void TearDown()
        {
            dataHelper.CleanUp();
        }

        [TestMethod]
        public void AddNewExerciseExpectExerciseAdded()
        {
            var exercise = _exerciseRepository.AddExercise("Barbell Bench Press");

            Assert.AreNotEqual(0, exercise.Id);
        }

        [TestMethod]
        public void Add2ExercisesWithTheSameNameExpect2ndExerciseToReturnThe1stId()
        {
            var exercise = _exerciseRepository.AddExercise("Barbell Bench Press");

            Assert.AreNotEqual(0, exercise.Id);

            var secondExercise = _exerciseRepository.AddExercise("Barbell Bench Press");

            Assert.AreEqual(exercise.Id, secondExercise.Id);
        }

        [TestMethod]
        public void GetExerciseByIdReturnsExercise()
        {
            var exercise = _exerciseRepository.AddExercise("Barbell Bench Press");

            var id = exercise.Id;

            var newExercise = _exerciseRepository.GetExerciseById(id);

            Assert.AreEqual(exercise.Title, newExercise.Title);
        }

        [TestMethod]
        public void GetExerciseThatDoesNotExistReturnNullAndDoesNotThrowError()
        {
            var exercise = _exerciseRepository.GetExerciseById(1);

            Assert.IsNull(exercise);
        }
    }
}
