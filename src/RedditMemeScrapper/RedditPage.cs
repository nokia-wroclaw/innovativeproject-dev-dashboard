using System.Collections.Generic;
using RestSharp.Deserializers;

namespace RedditMemeScrapper
{
    public class RedditPage
    {
        public List<string> PostIds { get; set; }
        public Dictionary<string, Post> Posts { get; set; }

        public int Dist { get; set; }
        public Source Source { get; set; }
    }

    public class Resolution
    {
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class Media
    {
        public object Obfuscated { get; set; }
        public string Content { get; set; }
        public int Width { get; set; }
        public List<Resolution> Resolutions { get; set; }
        public string Type { get; set; }
        public int Height { get; set; }
    }

    public class Post
    {
        public bool IsStickied { get; set; }
   
        public bool Saved { get; set; }
        public int NumComments { get; set; }
        public bool IsPinned { get; set; }
        public string Author { get; set; }
        public Media Media { get; set; }
        public int NumCrossposts { get; set; }
        public bool IsSponsored { get; set; }

    }

    public class Source
    {
        public string Url { get; set; }
        public string OutboundUrl { get; set; }
        public long? OutboundUrlCreated { get; set; }
        public string DisplayText { get; set; }
        public long? OutboundUrlExpiration { get; set; }
    }
}