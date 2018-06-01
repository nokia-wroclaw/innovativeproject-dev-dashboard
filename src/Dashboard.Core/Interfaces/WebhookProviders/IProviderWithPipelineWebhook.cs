using Dashboard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Core.Interfaces.WebhookProviders
{
    public interface IProviderWithPipelineWebhook
    {
        string ExtractProjectIdFromPipelineWebhook(object body);
        Pipeline ExtractPipelineFromWebhook(object body);
    }
}
