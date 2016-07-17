using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingTrackerAPI.Models
{
    public class ApiLog
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Title { get; set; }
        public DateTime DateAdded { get; set; }
        public int SetCount { get; set; }
    }
}