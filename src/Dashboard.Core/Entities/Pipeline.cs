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

        public DateTime LastUpdate { get; set; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Pipeline);
        }

        public bool Equals(Pipeline p)
        {
            // If parameter is null, return false.
            if (Object.ReferenceEquals(p, null))
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, p))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != p.GetType())
            {
                return false;
            }

            return Sha.Equals(p.Sha);
        }
    }
}
