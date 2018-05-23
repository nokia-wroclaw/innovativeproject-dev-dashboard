using Dashboard.Core.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Core.Interfaces.WebhookProviders
{
    public interface IProviderWithJobWebhook
    {
        Status RecalculateStageStatus(ICollection<Job> jobs);
        /// <summary>
        /// Should throw FormatException if body can't be parsed to Job
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        Job ExtractJobFromWebhook(object body);
    }
}
