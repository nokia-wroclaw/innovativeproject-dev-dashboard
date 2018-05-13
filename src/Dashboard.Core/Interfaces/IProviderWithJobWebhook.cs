using Dashboard.Core.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Core.Interfaces
{
    public interface IProviderWithJobWebhook
    {
        Status RecalculateStageStatus(ICollection<Job> jobs);
        Job ExtractJobFromWebhook(JObject body);
        //Task<IEnumerable<Stage>> ExtractUpdatedJobInfo(string apiHost, string apiKey, string apiProjectId, JObject body, IEnumerable<Stage> projectStages);
    }
}
