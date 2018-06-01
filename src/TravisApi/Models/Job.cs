using System;
using System.Collections.Generic;
using System.Text;
using RestSharp.Deserializers;

namespace TravisApi.Models
{
    public class Job
    {
        [DeserializeAs(Name = "@type")]
        public string Type { get; set; }

        [DeserializeAs(Name = "@href")]
        public string Href { get; set; }

        [DeserializeAs(Name = "@representation")]
        public string Representation { get; set; }

        public int Id { get; set; }
        public bool AllowFailure { get; set; }
        public string Number { get; set; }
        public string State { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public Build Build { get; set; }
        public string Queue { get; set; }
        public Repository Repository { get; set; }
        public Commit Commit { get; set; }
        public User Owner { get; set; }
        public object Stage { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Private { get; set; }
    }
}
