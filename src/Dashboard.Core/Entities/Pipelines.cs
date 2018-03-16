using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Core.Entities
{
    public class Pipelines : List<Pipeline>
    {
        //public List<Pipeline> AllPipelines { get; set; }
    }

    public class Pipeline
    {
        public UInt32 Id { get; set; }
        public string Sha{ get; set; }
        public string Ref { get; set; }
        public string Status { get; set; }
        //public User Owner { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }
        //public DateTime StartedAt { get; set; }
        //public DateTime FinishedAt { get; set; }
    }
}
