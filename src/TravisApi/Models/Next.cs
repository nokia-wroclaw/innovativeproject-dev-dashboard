using System;
using System.Collections.Generic;
using System.Text;

namespace TravisApi.Models
{
    public class PaginationNode
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}
