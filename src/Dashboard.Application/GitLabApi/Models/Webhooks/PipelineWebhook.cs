using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.GitLabApi.Models.Webhooks
{
    public class PipelineWebhook
    {
        public string ObjectKind { get; set; }
        public PipelineObjectAttributes ObjectAttributes { get; set; }
        public User User { get; set; }
        public Commit Commit { get; set; }
    }
}
