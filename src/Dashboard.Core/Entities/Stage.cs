using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Core.Entities
{
    public class Stage
    {
        public int Id { get; set; }
        public string StageName { get; set; }
        public virtual IEnumerable<Job> Jobs { get; set; } = new List<Job>();
    }
}
