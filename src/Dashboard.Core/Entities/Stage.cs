using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Dashboard.Core.Entities
{
    public class Stage
    {
        public int Id { get; set; }
        public string StageName { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }

        public Status StageStatus => Jobs.Any(p => p.Status == Status.Failed) ? Status.Failed :
            Jobs.Any(p => p.Status == Status.Running) ? Status.Running :
            Jobs.All(p => p.Status == Status.Canceled) ? Status.Canceled :
            Jobs.All(p => p.Status == Status.Success) ? Status.Success :
            Status.Created;
    }
}
