using System;
using System.Collections.Generic;
using System.Text;

namespace TravisApi.Models.Responses
{
    public class GetRepoBuildsResponse
    {
        public string Type { get; set; }
        public string Href { get; set; }
        public string Representation { get; set; }
        public Pagination Pagination { get; set; }
        public List<Build> Builds { get; set; }
    }
}
