using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrainingTrackerMVC.Models
{
    public class ExerciseViewModel
    {
        public Exercise ExerciseToAdd { get; set; }
        public List<Exercise> AllExercises { get; set; }
    }
}