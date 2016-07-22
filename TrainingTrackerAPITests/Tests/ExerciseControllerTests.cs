using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TrainingTrackerAPI.Controllers;
using Business;
using TrainingTrackerAPITests.Helpers;
using System.Web.Http.Results;
using System.Collections.Generic;
using TrainingTrackerAPI.Models;

namespace TrainingTrackerAPITests
{
    [TestClass]
    public class ExerciseControllerTests : BaseControllerTests
    {
        [TestInitialize]
        public void Initialize()
        {
            _mockExerciseBusiness = new Mock<IExerciseBusiness>();
            _exerciseController = new ExerciseController(_mockExerciseBusiness.Object);
        }

        [TestMethod]
        public void GetAllExercisesExpectExerciseListReturnedAnd200Ok()
        {
            var dataExerciseList = TestHelper.GetTestDataExerciseList();
            _mockExerciseBusiness.Setup(x => x.GetAllExercises()).Returns(dataExerciseList);

            var result = _exerciseController.GetAllExercises();

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<IList<ApiExercise>>));
        }
    }
}
