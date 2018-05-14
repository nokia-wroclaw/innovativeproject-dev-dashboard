using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.GitLabApi.Models
{
    public class JobWebhook
    {
        public string ObjectKind { get; set; }
        public string Ref { get; set; }
        public bool Tag { get; set; }
        public string BeforeSha { get; set; }
        public string Sha { get; set; }
        public int BuildId { get; set; }
        public string BuildName { get; set; }
        public string BuildStage { get; set; }
        public string BuildStatus { get; set; }
        //public DateTime? BuildStartedAt { get; set; }
        //public DateTime? BuildFinishedAt { get; set; }
        public double? BuildDuration { get; set; }
        public bool BuildAllowFailure { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public User User { get; set; }
        public Commit Commit { get; set; }
        public object Repository { get; set; } //No entity for this
    }
}
