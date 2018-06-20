using System;
using System.Collections.Generic;
using System.Text;
using Dashboard.Core.Entities;
using Newtonsoft.Json.Linq;

namespace Dashboard.Core.Interfaces.CiProviders
{
    public interface ICiWebhookProvider
    {
        DataProviderJobInfo ExtractJobInfo(JObject requestBody);
        DataProviderPipelineInfo ExtractPipelineInfo(JObject requestBody);
    }

    public class DataProviderJobInfo
    {
        public string ProviderName { get; set; }
        public string ProjectId { get; set; }

        public int JobId { get; set; }
        public Status Status { get; set; }
    }

    public class DataProviderPipelineInfo
    {
        public Status Status { get; set; }
        public string ProviderName { get; set; }
        public string ProjectId { get; set; }
        public string PipelineId { get; set; }
    }
}
