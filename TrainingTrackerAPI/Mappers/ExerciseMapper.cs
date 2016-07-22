using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingTrackerAPI.Models;

namespace TrainingTrackerAPI.Mappers
{
    public static class ExerciseMapper
    {
        public static ApiExercise DataToApiModel(Exercise exercise)
        {
            return new ApiExercise()
            {
                Id = exercise.Id,
                Title = exercise.Title
            };
        }

        public static IList<ApiExercise> DataListToApiList(IList<Exercise> exercises)
        {
            var apiExercises = new List<ApiExercise>();

            foreach (var exercise in exercises)
            {
                apiExercises.Add(DataToApiModel(exercise));
            };

            return apiExercises;
        }
    }
}