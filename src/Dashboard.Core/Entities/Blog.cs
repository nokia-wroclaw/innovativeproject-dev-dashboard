using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Core.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
