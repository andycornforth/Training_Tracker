using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Moq;
using Models;
using Business;
using System.Collections.Generic;
using Exceptions;
using System.Linq;

namespace BusinessTests
{
    [TestClass]
    public class SetBusinessTests
    {
        private Mock<ISetRepository> _mockSetRepository;
        private ISetBusiness _setBusiness;

        [TestInitialize]
        public void SetUp()
        {
            _mockSetRepository = new Mock<ISetRepository>();
            _setBusiness = new SetBusiness(_mockSetRepository.Object);
        }

        private IList<Set> _sets = new List<Set>()
        {
            new Set(),
            new Set()
        };

        [TestMethod]
        public void AddSetExpectNoErrorThrown()
        {
            var set = new Set()
            {
                Log = new Log(),
                Exercise = new Exercise(),
                PositionInLog = 1,
                Weight = 100,
                Reps = 20
            };

            _setBusiness.AddSetToLog(set);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Set not specified")]
        public void AddNullExpectBusninessErrorThrown()
        {
            _setBusiness.AddSetToLog(null);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "The set is not associated to a log")]
        public void AddSetWithNullLogExpectBusinessErrorThrown()
        {
            var set = new Set()
            {
                Exercise = new Exercise(),
                PositionInLog = 1,
                Weight = 100,
                Reps = 20
            };

            _setBusiness.AddSetToLog(set);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "The set is not associated to an exercise")]
        public void AddSetWithNullExerciseExpectBusinessErrorThrown()
        {
            var set = new Set()
            {
                Log = new Log(),
                PositionInLog = 1,
                Weight = 100,
                Reps = 20
            };

            _setBusiness.AddSetToLog(set);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "The sets position in the log must be greater than 0")]
        public void AddSetWithPositionInLog0ExpectErrorThrown()
        {
            var set = new Set()
            {
                Log = new Log(),
                Exercise = new Exercise(),
                PositionInLog = 0,
                Weight = 100,
                Reps = 20
            };

            _setBusiness.AddSetToLog(set);
        }

        [TestMethod]
        public void GetSetsForLogIdExpectLogsReturned()
        {
            _mockSetRepository.Setup(x => x.GetSetsByLogId(It.IsAny<int>())).Returns(_sets);

            var sets = _setBusiness.GetSetsByLogId(1);

            Assert.IsNotNull(sets);
            Assert.AreEqual(2, sets.Count);
        }

        [TestMethod]
        public void GetLatestSetsForLogExpectLogReturned()
        {
            _mockSetRepository.Setup(x => x.GetLatestSetForLog(It.IsAny<int>())).Returns(_sets.FirstOrDefault());

            var set = _setBusiness.GetLatestSetForLog(1);

            Assert.AreEqual(_sets.FirstOrDefault(), set);
        }

        [TestMethod]
        public void UpdateSetExpectNoErrorThrown()
        {
            var set = new Set()
            {
                Log = new Log(),
                Exercise = new Exercise(),
                PositionInLog = 1,
                Weight = 100,
                Reps = 20
            };

            _setBusiness.UpdateSet(set);

            _mockSetRepository.Verify(x => x.UpdateSet(It.IsAny<Set>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "The set is not associated to an exercise")]
        public void UpdateSetWithNullExerciseExpectBusinessErrorThrown()
        {
            var set = new Set()
            {
                Log = new Log(),
                PositionInLog = 1,
                Weight = 100,
                Reps = 20
            };

            _setBusiness.UpdateSet(set);
        }
    }
}
