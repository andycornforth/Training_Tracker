using Exceptions;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Repository
{
    public interface ISetRepository
    {
        void AddSet(Set set);
        IList<Set> GetSetsByLogId(int logId);
    }

    public class SetRepository : BaseSqlRepository, ISetRepository
    {
        public SetRepository() : base()
        {
        }

        public void AddSet(Set set)
        {
            var command = GetCommand("AddSet", CommandType.StoredProcedure);

            AddParameter(command, "@LogId", set.Log.Id);
            AddParameter(command, "@ExerciseId", set.Exercise.Id);
            AddParameter(command, "@Weight", set.Weight);
            AddParameter(command, "@Reps", set.Reps);
            AddParameter(command, "@Position", set.PositionInLog);

            ExecuteNonQuery(command);
        }

        public IList<Set> GetSetsByLogId(int logId)
        {
            var command = GetCommand("GetSetsByLogId", CommandType.StoredProcedure);

            AddParameter(command, "@LogId", logId);

            return GetEntitiesFromDatabase<Set>(command);
        }

        protected override object MapRowToEntity(IDataReader reader)
        {
            return new Set()
            {
                Id = reader.GetInt32(reader.GetOrdinal("SetId")),
                Log = new Log()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("LogId")),
                    PersonId = reader.GetInt32(reader.GetOrdinal("PersonId"))
                },
                Exercise = new Exercise()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ExerciseId")),
                    Title = reader.GetString(reader.GetOrdinal("Title"))
                },
                Weight = reader.GetDouble(reader.GetOrdinal("Weight")),
                Reps = reader.GetInt32(reader.GetOrdinal("Reps")),
                PositionInLog = reader.GetInt32(reader.GetOrdinal("PositionInLog"))
            };
        }
    }
}
