using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var command = GetCommand("AddLog", System.Data.CommandType.StoredProcedure);

            AddParameter(command, "@PersonId", log.PersonId);
            AddParameter(command, "@Title", log.Title);
            AddParameter(command, "@Date", DateTime.Now);

            ExecuteNonQueryChecked(command);
        }

        public Log GetLogById(int id)
        {
            var query = @"SELECT * FROM [Log] WHERE LogId = @LogId";
            var command = GetCommand(query, CommandType.Text);

            AddParameter(command, "@LogId", id);

            return GetEntitiesFromDatabase<Log>(command).FirstOrDefault();
        }

        public IList<Log> GetAllLogsByUserId(int userId)
        {
            var query = @"SELECT * FROM [Log] WHERE PersonId = @PersonId";
            var command = GetCommand(query, CommandType.Text);

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
