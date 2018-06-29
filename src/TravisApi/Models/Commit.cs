using System;
using System.Collections.Generic;
using System.Text;

namespace TravisApi.Models
{
    public class Commit
    {
        public int Id { get; set; }
        public string Sha { get; set; }
        public string Ref { get; set; }
        public string Message { get; set; }
        public string CompareUrl { get; set; }
        public DateTime CommittedAt { get; set; }
    }
}
