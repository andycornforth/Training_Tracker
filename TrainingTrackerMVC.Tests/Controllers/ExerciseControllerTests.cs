using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingTrackerMVC;
using TrainingTrackerMVC.Controllers;
using Moq;
using Business;
using Models;
using TrainingTrackerMVC.Models;

namespace TrainingTrackerMVC.Tests.Controllers
{
    [TestClass]
    public class ExerciseControllerTest
    {
        private Mock<IExerciseBusiness> _mockExerciseBusiness;
        private ExerciseController _exerciseController;
        private int _userId = 1;

        [TestInitialize]
        public void SetUp()
        {
            _mockExerciseBusiness = new Mock<IExerciseBusiness>();
        }

        private IList<Exercise> _exercises = new List<Exercise>()
        {
            new Exercise() { Id = 1, Title = "Bench Press" },
            new Exercise() { Id = 2, Title = "Squat" }
        };

        private void InitializeController()
        {
            _exerciseController = new ExerciseController(_mockExerciseBusiness.Object);
        }

        [TestMethod]
        public void ExerciseIndexReturnsValidViewResult()
        {
            _mockExerciseBusiness.Setup(x => x.GetAllExercises()).Returns(_exercises);

            InitializeController();

            var result = _exerciseController.Index(_userId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult));

            var actionResult = result as ViewResult;

            Assert.IsNotNull(actionResult.Model);
            Assert.AreEqual("Index", actionResult.ViewName);
        }

        [TestMethod]
        public void AddExerciseReturnsIndexViewResult()
        {
            _mockExerciseBusiness.Setup(x => x.GetAllExercises()).Returns(_exercises);

            InitializeController();

            var model = new ExerciseViewModel()
            {
                Log = new Log()
                {
                    Id = 1
                },
                ExerciseToAdd = new Exercise()
                {
                    Title = "Squat"
                }
            };

            var result = _exerciseController.AddExercise(model);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult));

            var actionResult = result as ViewResult;

            Assert.IsNotNull(actionResult.Model);
            Assert.AreEqual("Index", actionResult.ViewName);
        }
    }
}
