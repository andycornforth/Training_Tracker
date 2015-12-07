using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Set
    {
        public int Id { get; set; }
        public Log Log { get; set; }
        public Exercise Exercise { get; set; }
        public double Weight { get; set; }
        public int Reps { get; set; }
        public int PositionInLog { get; set; }
    }
}
