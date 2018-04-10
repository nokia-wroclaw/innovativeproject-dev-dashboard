using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.GitLabApi.Models
{
    public class CommitStats
    {
        public int Additions { get; set; }
        public int Deletions { get; set; }
        public int Total { get; set; }
    }
}
