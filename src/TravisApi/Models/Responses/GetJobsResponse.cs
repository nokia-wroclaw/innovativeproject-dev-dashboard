using System;
using System.Collections.Generic;
using System.Text;
using RestSharp.Deserializers;

namespace TravisApi.Models.Responses
{
    public class GetJobsResponse
    {
        [DeserializeAs(Name = "@type")]
        public string Type { get; set; }

        [DeserializeAs(Name = "@href")]
        public string Href { get; set; }

        [DeserializeAs(Name = "@representation")]
        public string Representation { get; set; }

        public List<Job> Jobs { get; set; }
    }
}
