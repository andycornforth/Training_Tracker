using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingTrackerMVC.Models
{
    public class LogViewModel
    {
        public Log Log { get; set; }
        public List<Set> Sets { get; set; }
    }
}