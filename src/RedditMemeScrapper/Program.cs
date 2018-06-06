using Newtonsoft.Json;
using RestSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using RedditMemeScraper;

namespace RedditMemeScrapper
{
    class Program
    {
        static void ShouldWork()
        {
            var scraper = new RedditScraper();

            var r = scraper.Scrap("memes", 50, options =>
            {
                options.AllowOver18 = true;
                options.SortMode = RedditSort.Hot;
                options.PerPage = 25;
            }).Result;

            //foreach (var s in r)
            //    Console.WriteLine(s.ImageUrl);

            var allJson = JsonConvert.SerializeObject(r);
            File.WriteAllText("result.json", allJson);

            Console.WriteLine("Done " + r.Count());
        }

        static void Main(string[] args)
        {
            ShouldWork();
            //var jsonString =
            //    File.ReadAllText(
            //        @"C:\Users\User\Source\Repos\innovativeproject-dev-dashboard\src\RedditMemeScrapper\ExamplePage.json");

            //SimpleJson.SimpleJson.CurrentJsonSerializerStrategy = new CamelCaseSerializerStrategy();
            //var redditPage = SimpleJson.SimpleJson.DeserializeObject<RedditPage>(jsonString);

            //Console.WriteLine(redditPage.Posts.Count);

            //var urls = redditPage.Posts.Values
            //    .Where(post => !post.IsSponsored && post.Media != null && post.Media.Obfuscated == null && post.Media.Type == "image")
            //    .Select(post => post.Media.Content);


            //foreach (var s in urls)
            //    Console.WriteLine(s);


            Console.ReadLine();
        }
    }
}
