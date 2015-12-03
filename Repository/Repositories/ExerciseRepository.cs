using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IExerciseRepository
    {
        Exercise AddExercise(string title);
        Exercise GetExerciseById(int id);
        IList<Exercise> GetAllExercises();
    }

    public class ExerciseRepository : BaseSqlRepository, IExerciseRepository
    {
        public ExerciseRepository() : base()
        {
        }

        public Exercise AddExercise(string title)
        {
            var command = GetCommand("AddExercise", CommandType.StoredProcedure);

            AddParameter(command, "@Title", title);

            return GetEntitiesFromDatabase<Exercise>(command).FirstOrDefault();
        }

        public Exercise GetExerciseById(int id)
        {
            var command = GetCommand("GetExercise", CommandType.StoredProcedure);

            AddParameter(command, "@Id", id);

            return GetEntitiesFromDatabase<Exercise>(command).FirstOrDefault();
        }

        public IList<Exercise> GetAllExercises()
        {
            var command = GetCommand("GetAllExercises", CommandType.StoredProcedure);

            return GetEntitiesFromDatabase<Exercise>(command);
        }

        protected override object MapRowToEntity(IDataReader reader)
        {
            return new Exercise()
            {
                Id = reader.GetInt32(reader.GetOrdinal("ExerciseId")),
                Title = reader.GetString(reader.GetOrdinal("Title"))
            };
        }
    }
}
