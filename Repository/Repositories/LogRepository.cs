using Exceptions;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Repository
{
    public interface ILogRepository
    {
        void AddLog(Log log);
        Log GetLogById(int id);
        IList<Log> GetAllLogsByUserId(int userId);
    }

    public class LogRepository : BaseSqlRepository, ILogRepository
    {
        public LogRepository() : base()
        {
        }

        public void AddLog(Log log)
        {
            if (log?.Title == null || log.Title.Equals(string.Empty))
                throw new RepositoryException("A log must have a title supplied");

            var command = GetCommand("AddLog", CommandType.StoredProcedure);

            AddParameter(command, "@PersonId", log.PersonId);
            AddParameter(command, "@Title", log.Title);
            AddParameter(command, "@Date", DateTime.Now);

            try {
                ExecuteNonQueryChecked(command);
            }
            catch (SqlException e) when (e.Number == 2627)//Unique key constraint
            {
                throw new RepositoryException($"The log '{log.Title}' already exists");
            }
        }

        public Log GetLogById(int id)
        {
            var command = GetCommand("GetLogById", CommandType.StoredProcedure);

            AddParameter(command, "@LogId", id);

            return GetEntitiesFromDatabase<Log>(command).FirstOrDefault();
        }

        public IList<Log> GetAllLogsByUserId(int userId)
        {
            var command = GetCommand("GetAllLogsByPersonId", CommandType.StoredProcedure);

            AddParameter(command, "@PersonId", userId);

            return GetEntitiesFromDatabase<Log>(command);
        }

        protected override object MapRowToEntity(IDataReader reader)
        {
            return new Log()
            {
                Id = reader.GetInt32(reader.GetOrdinal("LogId")),
                PersonId = reader.GetInt32(reader.GetOrdinal("PersonId")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                DateAdded = reader.GetDateTime(reader.GetOrdinal("DateAdded"))
            };
        }
    }
}
