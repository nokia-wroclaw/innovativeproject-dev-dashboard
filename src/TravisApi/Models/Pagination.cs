using System;
using System.Collections.Generic;
using System.Text;

namespace TravisApi.Models
{
    public class Pagination
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int Count { get; set; }
        public bool IsFirst { get; set; }
        public bool IsLast { get; set; }
        public PaginationNode Next { get; set; }
        public PaginationNode Prev { get; set; }
        public PaginationNode First { get; set; }
        public PaginationNode Last { get; set; }
    }
}
