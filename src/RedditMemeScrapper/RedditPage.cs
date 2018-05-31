using System;
using System.Collections.Generic;
using System.Text;

namespace RedditMemeScrapper
{
    public class RedditPage
    {
        public string[] postIds { get; set; }
        public int dist { get; set; }
        public Dictionary<string, Post> posts;
    }

    public class Resolution
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Media
    {
        public object obfuscated { get; set; }
        public string content { get; set; }
        public int width { get; set; }
        public List<Resolution> resolutions { get; set; }
        public string type { get; set; }
        public int height { get; set; }
    }

    public class Post
    {
        public bool isStickied { get; set; }
        public object domainOverride { get; set; }
        public object callToAction { get; set; }
        public List<object> eventsOnRender { get; set; }
        public bool saved { get; set; }
        public int numComments { get; set; }
        public object upvoteRatio { get; set; }
        public bool isPinned { get; set; }
        public string author { get; set; }
        public Media media { get; set; }
        public int numCrossposts { get; set; }
        public bool isSponsored { get; set; }
        public string id { get; set; }
        public Source source { get; set; }
        public bool isLocked { get; set; }
        public int score { get; set; }
        public bool isArchived { get; set; }
        public bool hidden { get; set; }
        public Thumbnail thumbnail { get; set; }
        public BelongsTo belongsTo { get; set; }
        public bool isRoadblock { get; set; }
        public object crosspostRootId { get; set; }
        public object crosspostParentId { get; set; }
        public bool sendReplies { get; set; }
        public int goldCount { get; set; }
        public bool isSpoiler { get; set; }
        public bool isNSFW { get; set; }
        public bool isMediaOnly { get; set; }
        public string postId { get; set; }
        public object suggestedSort { get; set; }
        public bool isBlank { get; set; }
        public int viewCount { get; set; }
        public string permalink { get; set; }
        public long created { get; set; }
        public string title { get; set; }
        public List<object> events { get; set; }
        public bool isOriginalContent { get; set; }
        public object distinguishType { get; set; }
        public int voteState { get; set; }
        public List<object> flair { get; set; }
    }

    public class Source
    {
        public string url { get; set; }
        public string outboundUrl { get; set; }
        public long outboundUrlCreated { get; set; }
        public string displayText { get; set; }
        public long outboundUrlExpiration { get; set; }
    }

    public class Thumbnail
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class BelongsTo
    {
        public string type { get; set; }
        public string id { get; set; }
    }
}
