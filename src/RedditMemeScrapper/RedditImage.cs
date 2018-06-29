using System;
using System.Collections.Generic;
using System.Text;

namespace RedditMemeScrapper
{
    public class RedditImage
    {
        public Uri ImageUrl { get; set; }
        public int Width { get; set; }
        public int Heigh { get; set; }
        public string Type { get; set; }

        public DateTime CreatedAt { get; } = DateTime.UtcNow;
    }
}
