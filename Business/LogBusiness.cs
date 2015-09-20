using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface ILogBusiness
    {
        void AddLogToDatabase(Log person);
        IList<Log> GetAllLogsByUsername(string username);
    }

    public class LogBusiness : ILogBusiness
    {
        private ILogRepository _logRepository;

        public LogBusiness()
        {
            _logRepository = new LogRepository(new ConnectionStringProvider());
        }

        public LogBusiness(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public void AddLogToDatabase(Log log)
        {
            if (log != null)
            {
                _logRepository.AddLog(log);
            }
        }

        public IList<Log> GetAllLogsByUsername(string username)
        {
            if (username == null)
                return null;

            return _logRepository.GetAllLogsByUsername(username);
        }
    }
}