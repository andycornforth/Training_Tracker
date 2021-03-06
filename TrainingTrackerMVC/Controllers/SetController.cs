﻿using Business;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrainingTrackerMVC.Controllers
{
    public class SetController : Controller
    {
        private IExerciseBusiness _exericseBusiness;
        private ILogBusiness _logBusiness;
        private ISetBusiness _setBusiness;

        public SetController(IExerciseBusiness exerciseBusiness, ILogBusiness logBusiness, ISetBusiness setBusiness)
        {
            _exericseBusiness = exerciseBusiness;
            _logBusiness = logBusiness;
            _setBusiness = setBusiness;
        }

        public ActionResult Index(int exerciseId, int logId, int? positionInLog, double? weight, int? reps)
        {
            var exercise = _exericseBusiness.GetExerciseById(exerciseId);
            var log = _logBusiness.GetLogById(logId);

            if (positionInLog <= log.SetCount)
            {
                return RedirectToAction("ViewLog", "Log", new { logId = logId });
            }

            var position = positionInLog ?? log.SetCount + 1;

            var model = new Set()
            {
                Log = log,
                Exercise = exercise,
                PositionInLog = position
            };

            if(weight != null & reps != null)
            {
                model.Weight = weight ?? 0;
                model.Reps = reps ?? 0;
            }

            return View(model);
        }

        public ActionResult UpdateSet(int exerciseId, int logId, double weight, int reps, int positionInLog)
        {
            var exercise = _exericseBusiness.GetExerciseById(exerciseId);
            var log = _logBusiness.GetLogById(logId);

            var model = new Set()
            {
                Log = log,
                Exercise = exercise,
                Weight = weight,
                Reps = reps,
                PositionInLog = positionInLog
            };

            return View("Index", model);
        }

        public ActionResult DeleteSet(int logId, int setId)
        {
            _setBusiness.DeleteSet(logId, setId);

            return RedirectToAction("ViewLog", "Log", new { logId = logId });
        }

        [HttpPost]
        public ActionResult Index(Set set)
        {
            ModelState.Clear();

            _setBusiness.AddSetToLog(set);

            var position = set.PositionInLog + 1;

            return Index(set.Exercise.Id, set.Log.Id, position, set.Weight, set.Reps);
        }

        private int GetLatestPositionInLog(int logId)
        {
            var set = _setBusiness.GetLatestSetForLog(logId);

            if (set == null)
            {
                return 1;
            }

            return set.PositionInLog + 1;
        }
    }
}