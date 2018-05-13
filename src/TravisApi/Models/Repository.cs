using System;
using System.Collections.Generic;
using System.Text;

namespace TravisApi.Models
{
    public class Repository
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}
