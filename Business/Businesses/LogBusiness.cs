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
    public interface ILogBusiness
    {
        int AddLogToDatabase(Log log);
        IList<Log> GetAllLogsByUserId(int userId);
        Log GetLogById(int id);
    }

    public class LogBusiness : ILogBusiness
    {
        private ILogRepository _logRepository;

        public LogBusiness(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public int AddLogToDatabase(Log log)
        {
            if (log == null)
                throw new BusinessException("Log cannot be null");

            return _logRepository.AddLog(log);
        }

        public IList<Log> GetAllLogsByUserId(int userId)
        {
            if (userId == 0)
                return null;

            return _logRepository.GetAllLogsByUserId(userId);
        }

        public Log GetLogById(int id) => _logRepository.GetLogById(id);
    }
}