using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Core.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string ApiHostUrl { get; set; }
        public string ApiProjectId { get; set; }

        public string ApiAuthenticationToken { get; set; }

        public string DataProviderName { get; set; }

        public virtual IEnumerable<Pipeline> Pipelines { get; set; }
    }
}
