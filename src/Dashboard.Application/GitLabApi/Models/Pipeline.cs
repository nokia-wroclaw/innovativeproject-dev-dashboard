using System;

namespace Dashboard.Application.GitLabApi.Models
{
    public class Pipeline
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Ref { get; set; }
        public string Sha { get; set; }
        public string BeforeSha { get; set; }
        public bool Tag { get; set; }
        public object YamlErrors { get; set; }
        public User User { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public DateTime? CommittedAt { get; set; }
        public object Duration { get; set; }
        public string Coverage { get; set; }
    }
}
