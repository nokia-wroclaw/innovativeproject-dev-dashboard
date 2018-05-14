using Dashboard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Core.Interfaces.WebhookProviders
{
    public interface IProviderWithPipelineWebhook
    {
        Pipeline ExtractPipelineFromWebhook(object body);
    }
}
