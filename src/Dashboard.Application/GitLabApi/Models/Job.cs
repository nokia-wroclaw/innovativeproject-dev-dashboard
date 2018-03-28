using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.GitLabApi.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Stage { get; set; }
        public string Name { get; set; }
        public string Ref { get; set; }
        public bool Tag { get; set; }
        public double? Coverage { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public double? Duration { get; set; }
        public User User { get; set; }
        public Commit Commit { get; set; }
        public Pipeline Pipeline { get; set; }
        public object Runner { get; set; }
    }
}
