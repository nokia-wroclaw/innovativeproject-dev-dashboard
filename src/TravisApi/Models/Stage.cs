using System;
using System.Collections.Generic;
using System.Text;

namespace TravisApi.Models
{
    public class Stage
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
    }
}
