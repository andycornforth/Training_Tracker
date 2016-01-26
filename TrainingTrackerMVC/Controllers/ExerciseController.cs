using Business;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingTrackerMVC.Models;

namespace TrainingTrackerMVC.Controllers
{
    public class ExerciseController : Controller
    {
        private IExerciseBusiness _exerciseBusiness;

        public ExerciseController(IExerciseBusiness exerciseBusiness)
        {
            _exerciseBusiness = exerciseBusiness;
        }

        public ActionResult Index(int logId)
        {
            var model = new ExerciseViewModel()
            {
                Log = new Log()
                {
                    Id = logId
                },
                AllExercises = _exerciseBusiness.GetAllExercises().ToList()
            };

            return View("Index", model);
        }

        public ActionResult AddExercise(ExerciseViewModel model)
        {
            var exercise = _exerciseBusiness.GetExercise(model.ExerciseToAdd.Title);

            return RedirectToAction("Index", "Set", new { exerciseId = exercise.Id, logId = model.Log.Id });
        }

        public ActionResult GetExercise(int exerciseId, int logId)
        {
            return RedirectToAction("Index", "Set", new { exerciseId = exerciseId, logId = logId });
        }
    }
}