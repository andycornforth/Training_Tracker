using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingTrackerAPI.Models;

namespace TrainingTrackerAPI.Mappers
{
    public static class LogMapper
    {
        public static Log ApiToDataModel(ApiLog apiLog)
        {
            return new Log()
            {
                Id = apiLog.Id,
                PersonId = apiLog.Id,
                Title = apiLog.Title,
                DateAdded = apiLog.DateAdded,
                SetCount = apiLog.SetCount
            };
        }

        public static ApiLog DataToApiModel(Log log)
        {
            return new ApiLog()
            {
                Id = log.Id,
                PersonId = log.Id,
                Title = log.Title,
                DateAdded = log.DateAdded,
                SetCount = log.SetCount
            };
        }
    }
}