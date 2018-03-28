using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.GitLabApi.Models
{
    public class Tag
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public Commit Commit { get; set; }
        //public Tag Release { get; set; }
    }
}
