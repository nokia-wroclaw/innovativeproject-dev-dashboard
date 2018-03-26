using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.GitLabApi.Models
{
    public class Commit
    {
        public string Id { get; set; }
        public string ShortId { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string CommitterName { get; set; }
        public string CommitterEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Message { get; set; }
        public DateTime CommitedDate { get; set; }
        public DateTime AuthoredDate { get; set; }
        public List<string> ParentIds { get; set; }
        public Pipeline LastPipeline { get; set; }
        public CommitStats Stats { get; set; }
        public string Status { get; set; }
    }
}
