using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Core.Entities
{
    public class PanelInformations
    {
        public string ProjectPathWithNamespace { get; set; }
        public string Branch { get; set; }
        public string PipelineStatus { get; set; }
        public string CommitTitle { get; set; }
        public string CommiterName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Tag { get; set; }
    }
}
