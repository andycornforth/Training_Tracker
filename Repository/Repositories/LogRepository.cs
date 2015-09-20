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
        IList<Log> GetAllLogsByUsername(string username);
    }

    public class LogRepository : BaseSqlRepository, ILogRepository
    {
        public LogRepository(IConnectionStringProvider connectionStringProvider)
            : base(connectionStringProvider)
        {
        }

        public void AddLog(Log log)
        {
            var command = GetCommand("AddLog", System.Data.CommandType.StoredProcedure);

            AddParameter(command, "@Username", log.Username);
            AddParameter(command, "@Title", log.Title);

            ExecuteNonQueryChecked(command);
        }

        public Log GetLogById(int id)
        {
            var query = @"SELECT * FROM [Log] WHERE LogId = @LogId";
            var command = GetCommand(query, CommandType.Text);

            AddParameter(command, "@LogId", id);

            return GetEntitiesFromDatabase<Log>(command).FirstOrDefault();
        }

        public IList<Log> GetAllLogsByUsername(string username)
        {
            var query = @"SELECT * FROM [Log] WHERE Username = @Username";
            var command = GetCommand(query, CommandType.Text);

            AddParameter(command, "@Username", username);

            return GetEntitiesFromDatabase<Log>(command);
        }

        protected override object MapRowToEntity(IDataReader reader)
        {
            return new Log()
            {
                Id = reader.GetInt32(reader.GetOrdinal("LogId")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                Title = reader.GetString(reader.GetOrdinal("Title"))
            };
        }
    }
}
