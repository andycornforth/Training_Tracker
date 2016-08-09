using Business;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrainingTrackerAPI.Mappers;
using TrainingTrackerAPI.Models;

namespace TrainingTrackerAPI.Controllers
{
    public class ExerciseController : ApiController
    {
        private IExerciseBusiness _exerciseBusiness;

        public ExerciseController(IExerciseBusiness exerciseBusiness)
        {
            _exerciseBusiness = exerciseBusiness;
        }

        public IHttpActionResult GetAllExercises()
        {
            var apiList = ExerciseMapper.DataListToApiList(_exerciseBusiness.GetAllExercises());
            return Ok(apiList);
        }

        public IHttpActionResult GetExerciseByTitle(string title)
        {
            var apiExercise = ExerciseMapper.DataToApiModel(_exerciseBusiness.GetExercise(title));
            return Ok(apiExercise);
        }

        public IHttpActionResult GetExerciseById(int id)
        {
            var apiExercise = ExerciseMapper.DataToApiModel(_exerciseBusiness.GetExerciseById(id));
            return Ok(apiExercise);
        }
    }
}
