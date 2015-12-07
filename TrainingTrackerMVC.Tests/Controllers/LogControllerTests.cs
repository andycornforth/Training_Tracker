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
using Exceptions;

namespace TrainingTrackerMVC.Tests.Controllers
{
    [TestClass]
    public class LogControllerTest
    {
        private Mock<ILogBusiness> _mockLogBusiness;
        private LogController _logController;
        private int _userId = 1;
        
        [TestInitialize]
        public void SetUp()
        {
            _mockLogBusiness = new Mock<ILogBusiness>();
        }

        private IList<Log> _logs = new List<Log>()
        {
           new Log() { Id = 1, PersonId = 1, Title = "Squat 1RM", DateAdded = new DateTime(2015,12,5)}
        };

        private void InitializeController()
        {
            _logController = new LogController(_mockLogBusiness.Object);
        }

        [TestMethod]
        public void ExerciseIndexReturnsValidViewResult()
        {
            InitializeController();

            var result = _logController.Index(_userId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult));

            var actionResult = result as ViewResult;

            Assert.AreEqual("", actionResult.ViewName);
        }

        [TestMethod]
        public void ViewAllLogsReturnsValidViewResultWithModel()
        {
            _mockLogBusiness.Setup(x => x.GetAllLogsByUserId(It.IsAny<int>())).Returns(_logs);

            InitializeController();

            var result = _logController.ViewAllLogs(_userId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult));

            var actionResult = result as ViewResult;

            Assert.AreEqual("", actionResult.ViewName);
            Assert.IsNotNull(actionResult.Model);
        }

        [TestMethod]
        public void AddLogViewReturnsValidViewResultWithModel()
        {
            InitializeController();

            var result = _logController.AddLogView(_userId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult));

            var actionResult = result as ViewResult;

            Assert.AreEqual("AddNewLog", actionResult.ViewName);
            Assert.IsNotNull(actionResult.Model);
        }

        [TestMethod]
        public void AddLogPostReturnsExerciseViewResult()
        {
            InitializeController();

            var model = new Log()
            {
                PersonId = _userId,
                Title = "test log"
            };

            var result = _logController.AddLog(model);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

            var actionResult = result as RedirectToRouteResult;

            Assert.AreEqual(1, actionResult.RouteValues.Values.ElementAt(0));
            Assert.AreEqual("Index", actionResult.RouteValues.Values.ElementAt(1));
            Assert.AreEqual("Exercise", actionResult.RouteValues.Values.ElementAt(2));

        }

        [TestMethod]
        public void AddLogPostWithDuplicateLogReturnsViewResultWithError()
        {
            _mockLogBusiness.Setup(x => x.AddLogToDatabase(It.IsAny<Log>())).Throws(new RepositoryException("Mock Error"));

            InitializeController();

            var model = new Log()
            {
                PersonId = _userId,
                Title = "test log"
            };

            var result = _logController.AddLog(model);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult));

            var actionResult = result as ViewResult;

            Assert.AreEqual("AddNewLog", actionResult.ViewName);

            Assert.AreEqual("There seems to be a problem with your request. Mock Error.", 
                actionResult.ViewData.ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage);
        }
    }
}
