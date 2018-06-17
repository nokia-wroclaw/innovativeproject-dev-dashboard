using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
}
