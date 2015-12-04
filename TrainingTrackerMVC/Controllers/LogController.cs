using Business;
using Exceptions;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrainingTrackerMVC.Controllers
{
    public class LogController : Controller
    {
        private ILogBusiness _logBusiness;

        public LogController(ILogBusiness logBusiness)
        {
            _logBusiness = logBusiness;
        }

        public ActionResult Index()
        {
            //if (System.Web.HttpContext.Current.Session["Username"] == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            return View();
        }

        public ActionResult ViewAllLogs()
        {
            var userId = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]);
            var allLogs = _logBusiness.GetAllLogsByUserId(userId);

            return View(allLogs);
        }

        public ActionResult AddLogView()
        {
            return View("AddNewLog");
        }

        public ActionResult AddLog(Log model)
        {
            model.PersonId = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]);

            try
            {
                _logBusiness.AddLogToDatabase(model);
            }
            catch (Exception e) when (e is BusinessException || e is RepositoryException)
            {
                ModelState.AddModelError("", e.Message);
                return View("AddNewLog", model);
            }
            return RedirectToAction("Index", "Exercise");
        }
    }
}