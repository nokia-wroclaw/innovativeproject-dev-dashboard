using System;
using System.Collections.Generic;

namespace TravisApi.Models
{
    public class Build
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string State { get; set; }
        public int? Duration { get; set; }
        public string EventType { get; set; }
        public string PreviousState { get; set; }
        public string PullRequestTitle { get; set; }
        public int? PullRequestNumber { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public bool Private { get; set; }
        public Repository Repository { get; set; }
        public Branch Branch { get; set; }
        public object Tag { get; set; }
        public Commit Commit { get; set; }
        public List<Job> Jobs { get; set; }
        public List<Stage> Stages { get; set; }
        public User CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
