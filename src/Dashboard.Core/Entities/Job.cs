using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dashboard.Core.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public int DataProviderJobId { get; set; }
        public Status Status { get; set; }
        public string StageName { get; set; }

        public virtual Stage Stage { get; set; }
    }
}
