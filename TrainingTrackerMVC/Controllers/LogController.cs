using Business;
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

        public LogController()
        {
            _logBusiness = new LogBusiness();
        }

        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.Session["Username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult ViewAllLogs()
        {
            if (System.Web.HttpContext.Current.Session["Username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var allLogs = _logBusiness.GetAllLogsByUsername(System.Web.HttpContext.Current.Session["Username"].ToString()).ToList();

            return View(allLogs);
        }

        public ActionResult AddLogView()
        {
            if (System.Web.HttpContext.Current.Session["Username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("AddNewLog");
        }

        public ActionResult AddLog(Log model)
        {
            if (System.Web.HttpContext.Current.Session["Username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            model.Username = System.Web.HttpContext.Current.Session["Username"].ToString();

            _logBusiness.AddLogToDatabase(model);

            return View("Index");
        }
    }
}