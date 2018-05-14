using System;
using System.Collections.Generic;
using System.Text;

namespace TravisApi.Models.Responses
{
    public class WebhookResponse
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public Repository Repository { get; set; }
    }
}
