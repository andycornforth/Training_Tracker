using System;

namespace Models
{
    public class Log
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Title { get; set; }
        public DateTime DateAdded { get; set; }
        public int SetCount { get; set; }
    }
}
