using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.GitLabApi.Models.Webhooks
{
    public class PipelineObjectAttributes
    {
        public int Id { get; set; }
        public string Ref { get; set; }
        public bool Tag { get; set; }
        public string Sha { get; set; }
        public string BeforeSha { get; set; }
        public string Status { get; set; }
        public string DetailedStatus { get; set; }
        public List<string> Stages { get; set; }
    }
}
