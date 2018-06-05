using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Core.Entities
{
    public class Stage
    {
        public int Id { get; set; }
        public string StageName { get; set; }
        public Status StageStatus { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }

    public class ResponseStage : Stage
    {
        public int Succeeded { get; set; }
        public int Total { get; set; }
    }
}
