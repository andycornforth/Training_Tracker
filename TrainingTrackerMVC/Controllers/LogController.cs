using Business;
using Exceptions;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingTrackerMVC.Models;

namespace TrainingTrackerMVC.Controllers
{
    public class LogController : Controller
    {
        private ILogBusiness _logBusiness;

        public LogController(ILogBusiness logBusiness)
        {
            _logBusiness = logBusiness;
        }

        public ActionResult Index(int userId)
        {
            return View(new User() { Id = userId });
        }

        [HttpGet]
        public ActionResult ViewAllLogs(int userId)
        {
            var allLogs = _logBusiness.GetAllLogsByUserId(userId);

            return View(allLogs);
        }

        [HttpGet]
        public ActionResult AddLogView(int userId)
        {
            return View("AddNewLog", new Log() { PersonId = userId });
        }

        [HttpPost]
        public ActionResult AddLog(Log model)
        {
            try
            {
                _logBusiness.AddLogToDatabase(model);
            }
            catch (Exception e) when (e is BusinessException || e is RepositoryException)
            {
                ModelState.AddModelError("", e.Message);
                return View("AddNewLog", model);
            }
            return RedirectToAction("Index", "Exercise", new { userId = model.PersonId});
        }
    }
}