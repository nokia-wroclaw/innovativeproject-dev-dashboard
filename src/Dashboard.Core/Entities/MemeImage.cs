using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Core.Entities
{
    public class MemeImage
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Heigh { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; }
    }
}
