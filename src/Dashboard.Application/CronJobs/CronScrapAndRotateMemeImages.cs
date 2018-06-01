using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.Repositories;
using RedditMemeScrapper;

namespace Dashboard.Application.CronJobs
{
    public class CronScrapAndRotateMemeImages
    {
        private readonly IMemeImageRepository _memeImageRepository;
        private readonly RedditScraper _scraper;

        public CronScrapAndRotateMemeImages(IMemeImageRepository memeImageRepository, RedditScraper scraper)
        {
            _memeImageRepository = memeImageRepository;
            _scraper = scraper;
        }

        public async Task PerformWork(string subreddit, int pages)
        {
            var redditImages = await _scraper.Scrap(subreddit, pages, o =>
            {
                o.AllowOver18 = true;
                o.PerPage = 25;
                o.SortMode = RedditSort.Hot;
            });

            var newImages = redditImages.Select(r => new MemeImage()
            {
                ImageUrl = r.ImageUrl.ToString(),
                Width = r.Width,
                Heigh = r.Heigh,
                CreatedAt = r.CreatedAt
            });
            var allDbImages = await _memeImageRepository.GetAllAsync();

            _memeImageRepository.DeleteRange(allDbImages);
            _memeImageRepository.AddRange(newImages);


            await _memeImageRepository.SaveAsync();
        }
    }
}
