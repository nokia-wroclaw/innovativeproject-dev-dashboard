using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RestSharp.Deserializers;

namespace TravisApi.Models.Responses
{
    public class GetRepoBuildsResponse
    {
        [DeserializeAs(Name = "@type")]
        public string Type { get; set; }

        [DeserializeAs(Name = "@href")]
        public string Href { get; set; }

        [DeserializeAs(Name = "@representation")]
        public string Representation { get; set; }

        [DeserializeAs(Name = "@pagination")]
        public Pagination Pagination { get; set; }

        public List<Build> Builds { get; set; }
    }
}
