using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business;
using Moq;
using TrainingTrackerAPI.Controllers;

namespace TrainingTrackerAPITests
{
    [TestClass]
    public class BaseControllerTests
    {
        protected PersonController _personController;
        protected LogController _logController;
        protected ExerciseController _exerciseController;

        protected Mock<IPersonBusiness> _mockPersonBusiness;
        protected Mock<ILogBusiness> _mockLogBusiness;
        protected Mock<IExerciseBusiness> _mockExerciseBusiness;
    }
}
