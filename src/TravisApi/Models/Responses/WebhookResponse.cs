using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TravisApi.Models.Responses
{
    public class WebhookResponse
    {
        public int Id { get; set; }
        public string State { get; set; }
        public Repository Repository { get; set; }
    }

    public class WebhookResponsePayload
    {
        public string Payload { get; set; }
    }
}
