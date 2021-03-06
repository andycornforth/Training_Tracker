﻿using Exceptions;
using Models;
using Repository.Helpers;
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
        Set GetLatestSetForLog(int logId);
        void DeleteSet(int logId, int setId);
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

            try
            {
                ExecuteNonQuery(command);
            }
            catch (SqlException)
            {
                throw new RepositoryException("Unable to process request. Database error.");
            }
        }

        public IList<Set> GetSetsByLogId(int logId)
        {
            var command = GetCommand("GetSetsByLogId", CommandType.StoredProcedure);

            AddParameter(command, "@LogId", logId);

            return GetEntitiesFromDatabase<Set>(command);
        }

        public Set GetLatestSetForLog(int logId)
        {
            var command = GetCommand("GetLatestSetForLog", CommandType.StoredProcedure);

            AddParameter(command, "@LogId", logId);

            return GetEntitiesFromDatabase<Set>(command).FirstOrDefault();
        }

        public void DeleteSet(int logId, int setId)
        {
            var command = GetCommand("DeleteSet", CommandType.StoredProcedure);

            AddParameter(command, "@LogId", logId);
            AddParameter(command, "@SetId", setId);

            try
            {
                ExecuteNonQuery(command);
            }
            catch (SqlException)
            {
                throw new RepositoryException("Unable to delete set. Database error.");
            }
        }

        protected override object MapRowToEntity(IDataReader reader)
        {
            return new Set()
            {
                Id = reader.GetInt32(reader.GetOrdinal("SetId")),
                Log = new Log()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("LogId")),
                    PersonId = reader.HasColumn("PersonId") ? reader.GetInt32(reader.GetOrdinal("PersonId")) : 0
                },
                Exercise = new Exercise()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ExerciseId")),
                    Title = reader.HasColumn("Title") ? reader.GetString(reader.GetOrdinal("Title")) : null
                },
                Weight = reader.GetDouble(reader.GetOrdinal("Weight")),
                Reps = reader.GetInt32(reader.GetOrdinal("Reps")),
                PositionInLog = reader.GetInt32(reader.GetOrdinal("PositionInLog"))
            };
        }
    }
}
