using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.GitLabApi.Models
{
    public class Release
    {
        public string TagName { get; set; }
        public string Description { get; set; }
    }
}
