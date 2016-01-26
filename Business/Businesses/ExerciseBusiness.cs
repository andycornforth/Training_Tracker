using Exceptions;
using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IExerciseBusiness
    {
        IList<Exercise> GetAllExercises();
        Exercise GetExercise(string title);
        Exercise GetExerciseById(int id);
    }

    public class ExerciseBusiness : IExerciseBusiness
    {
        private IExerciseRepository _exerciseRepository;

        public ExerciseBusiness(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public IList<Exercise> GetAllExercises() => _exerciseRepository.GetAllExercises();

        public Exercise GetExercise(string title)
        {
            if (title == null || title.Equals(string.Empty))
                throw new BusinessException("Cannot create an exercise without a title");

            return _exerciseRepository.AddExercise(title);
        }

        public Exercise GetExerciseById(int id)
        {
            var exercise = _exerciseRepository.GetExerciseById(id);

            if (exercise == null)
                throw new BusinessException("The exercise does not exist");

            return exercise;
        }
    }
}