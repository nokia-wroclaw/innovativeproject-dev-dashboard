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
        public int Id { get; set; }

        public int DataProviderId { get; set; }
        public string ProjectId { get; set; }
        public string Sha{ get; set; }
        public string Ref { get; set; }
        public string Status { get; set; }

        public string CommitTitle { get; set; }
        public string CommiterName { get; set; }
        public string CommiterEmail { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }

        public  List<Stage> Stages { get; set; }
    }
}
