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

        public ActionResult Index()
        {
            var model = new ExerciseViewModel()
            {
                AllExercises = _exerciseBusiness.GetAllExercises().ToList()
            };

            return View("Index", model);
        }

        public ActionResult AddExercise(ExerciseViewModel model)
        {
            var exercise = _exerciseBusiness.AddExercise(model.ExerciseToAdd.Title);

            //Redirect to action -> Choose weight -> choose reps -> choose sets (whether the same or diffe)
            return Index();
        }
    }
}