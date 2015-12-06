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
        void AddLogToDatabase(Log log);
        IList<Log> GetAllLogsByUserId(int userId);
    }

    public class LogBusiness : ILogBusiness
    {
        private ILogRepository _logRepository;

        public LogBusiness(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public void AddLogToDatabase(Log log)
        {
            if (log == null)
                throw new BusinessException("Log cannot be null");

            _logRepository.AddLog(log);
        }

        public IList<Log> GetAllLogsByUserId(int userId)
        {
            if (userId == 0)
                return null;

            return _logRepository.GetAllLogsByUserId(userId);
        }
    }
}