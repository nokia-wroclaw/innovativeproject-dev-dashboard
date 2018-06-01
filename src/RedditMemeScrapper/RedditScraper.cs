using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace RedditMemeScrapper
{
    public class RedditScraper
    {
        public TimeSpan DelayBetweenRequests { get; set; } = TimeSpan.FromSeconds(2);

        private RestClient _client = new RestClient("https://gateway.reddit.com");

        public async Task<IEnumerable<RedditImage>> Scrap(string subreddit, int pagesCount, Action<ScrapOptions> options)
        {
            var opts = new ScrapOptions();
            options(opts);

            var result = new List<RedditImage>();
            var currentPage = 0;
            while (++currentPage <= pagesCount)
            {
                await Task.Delay(DelayBetweenRequests);

                var pageResponse = await FetchPage(subreddit, opts);
                if (!pageResponse.IsSuccessful)
                    throw new HttpRequestException(pageResponse.ErrorMessage);

                opts.After = pageResponse.Data.PostIds.Last();

                var pageImages = ExtractImages(pageResponse.Data);

                result.AddRange(pageImages);
            }

            return result;
        }

        private Task<IRestResponse<RedditPage>> FetchPage(string subreddit, ScrapOptions options)
        {
            var request = new RestRequest($"/desktopapi/v1/subreddits/{subreddit}", Method.GET);
            
            request.AddQueryParameter("redditWebClient", "web2x");
            request.AddQueryParameter("app", "web2x-client-production");

            request.AddQueryParameter("sort", options.SortMode.ToString().ToLower());
            request.AddQueryParameter("dist", options.PerPage.ToString());

            if (string.IsNullOrEmpty(options.After))
                request.AddQueryParameter("after", options.After);

            if (options.AllowOver18)
                request.AddQueryParameter("allow_over18", "true");

            return _client.ExecuteTaskAsync<RedditPage>(request);
        }

        private IEnumerable<RedditImage> ExtractImages(RedditPage page)
        {
            return page.Posts.Values
                .Where(post => !post.IsSponsored && post.Media != null && post.Media.Obfuscated == null && post.Media.Type == "image")
                .Select(post => new RedditImage()
                {
                    ImageUrl = new Uri(post.Media.Content),
                    Heigh = post.Media.Height,
                    Width = post.Media.Width,
                    Type = post.Media.Type
                });
        }
    }

    public class ScrapOptions
    {
        public bool AllowOver18 { get; set; } = false;
        public RedditSort SortMode { get; set; } = RedditSort.Hot;
        public int PerPage { get; set; } = 25;
        public string After { get; set; } = string.Empty;
        public int MaxImagesCount { get; set; } = int.MaxValue;
    }

    public enum RedditSort {
        Hot,
        New,
        Controversial,
        Rising
    }
}
