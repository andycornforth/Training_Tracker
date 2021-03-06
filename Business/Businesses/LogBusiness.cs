﻿using Exceptions;
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

        /// <summary>
        /// Adds a Log to the database and returns the Log Id
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public int AddLogToDatabase(Log log)
        {
            if (log == null)
                throw new BusinessException("Log cannot be null");

            return _logRepository.AddLog(log);
        }

        /// <summary>
        /// Returns all the logs for a specific User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<Log> GetAllLogsByUserId(int userId)
        {
            if (userId == 0)
                return null;

            return _logRepository.GetAllLogsByUserId(userId);
        }

        /// <summary>
        /// Returns the specific log by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Log GetLogById(int id) => _logRepository.GetLogById(id);
    }
}