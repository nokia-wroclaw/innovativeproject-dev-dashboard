using System;
using System.Collections.Generic;
using System.Text;

namespace TravisApi.Models
{
    public class GetBranchResponse
    {
        public string Name { get; set; }
        public Repository Repository { get; set; }
        public bool DefaultBranch { get; set; }
        public bool ExistsOnGithub { get; set; }
        public Build LastBuild { get; set; }
    }
}
