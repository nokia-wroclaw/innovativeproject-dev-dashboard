using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Core.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectTitle { get; set; }
        public string ApiHostUrl { get; set; }
        public string ApiProjectId { get; set; }
        public string ApiAuthenticationToken { get; set; }
        public string DataProviderName { get; set; }
        public string CiDataUpdateCronExpression { get; set; }

        public virtual ICollection<Pipeline> StaticPipelines { get; set; }
        public virtual ICollection<Pipeline> DynamicPipelines { get; set; }
    }
}
