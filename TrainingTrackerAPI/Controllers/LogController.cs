using Business;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrainingTrackerAPI.Mappers;
using TrainingTrackerAPI.Models;

namespace TrainingTrackerAPI.Controllers
{
    public class LogController : ApiController
    {
        private ILogBusiness _logBusiness;

        public LogController(ILogBusiness logBusiness)
        {
            _logBusiness = logBusiness;
        }

        public IHttpActionResult AddLog(ApiLog log)
        {
            var dataLog = LogMapper.ApiToDataModel(log);
            var logId = _logBusiness.AddLogToDatabase(dataLog);
            return Ok(logId);
        }
    }
}
