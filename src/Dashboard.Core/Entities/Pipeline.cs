using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Core.Entities
{
    /// <summary>
    /// Pipeline
    ///     Stages
    /// </summary>
    public class Pipeline
    {
        public int Id { get; set; }

        public int DataProviderPipelineId { get; set; }
        public string ProjectId { get; set; }
        public string Sha{ get; set; }
        public string Ref { get; set; }
        public Status Status { get; set; }

        public string CommitTitle { get; set; }
        public string CommiterName { get; set; }
        public string CommiterEmail { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }

        public virtual ICollection<Stage> Stages { get; set; }
    }
}
