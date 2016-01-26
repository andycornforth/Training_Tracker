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
        private ISetBusiness _setBusiness;

        public LogController(ILogBusiness logBusiness, ISetBusiness setBusiness)
        {
            _logBusiness = logBusiness;
            _setBusiness = setBusiness;
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
        public ActionResult AddLogView(int userId) => View("AddNewLog", new Log() { PersonId = userId });

        [HttpPost]
        public ActionResult AddLog(Log model)
        {
            try
            {
                var id = _logBusiness.AddLogToDatabase(model);
                return RedirectToAction("Index", "Exercise", new { logId = id });
            }
            catch (Exception e) when (e is BusinessException || e is RepositoryException)
            {
                ModelState.AddModelError("", e.Message);
                return View("AddNewLog", model);
            }
        }

        [HttpGet]
        public ActionResult AddToLog(int logId) => RedirectToAction("Index", "Exercise", new { logId = logId });

        [HttpGet]
        public ActionResult ViewLog(int logId)
        {
            var log = _logBusiness.GetLogById(logId);
            var sets = _setBusiness.GetSetsByLogId(logId).ToList();

            var model = new LogViewModel()
            {
                Log = log,
                Sets = sets
            };

            return View(model);
        }
    }
}