using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Core.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public int DataProviderJobId { get; set; }
        public Status Status { get; set; }
        public string Stage { get; set; }
    }
}
