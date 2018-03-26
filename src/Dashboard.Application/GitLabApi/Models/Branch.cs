using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.GitLabApi.Models
{
    public class Branch
    {
        public string Name { get; set; }
        public bool Merged { get; set; }
        public bool Protected { get; set; }
        public bool DevelopersCanPush { get; set; }
        public bool DevelopersCanMerge { get; set; }
        public Commit Commit { get; set; }
    }
}
