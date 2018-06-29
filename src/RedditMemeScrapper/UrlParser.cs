using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RedditMemeScrapper
{
    public class UrlParser
    {
        private readonly IEnumerable<string> _urlPatterns = new List<string>()
        {
            @"(?<url>http[s]?:\/\/(?:i.)?imgur.com\/[A-Za-z0-9]{0,33}(?:.png|.jpg|.gif[v]?)?)"
        };

        public IEnumerable<Uri> Parse(string page)
        {
            var joinedPatterns = string.Join("|", _urlPatterns);

            var r = Regex.Match(page, joinedPatterns);

            return r.Groups["url"]
                .Captures
                .Select(url => new Uri(url.Value));
        }
    }
}
