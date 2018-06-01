using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Core.Interfaces.Repositories;

namespace Dashboard.Application.CronJobs
{
    public class CronRefreshMemePanelsImage
    {
        private readonly IMemePanelRepository _memePanelRepository;
        private readonly IMemeImageRepository _memeImageRepository;

        public CronRefreshMemePanelsImage(IMemePanelRepository memePanelRepository, IMemeImageRepository memeImageRepository)
        {
            _memePanelRepository = memePanelRepository;
            _memeImageRepository = memeImageRepository;
        }

        public async Task PerformRefresh()
        {
            var memePanels = (await _memePanelRepository.GetAllAsync()).ToList();
            var memeImages = (await _memeImageRepository.GetRandomMemes(memePanels.Count)).ToArray();

            if (memePanels.Count != memeImages.Length)
            {
                //There is not enough images
                return;
            }

            foreach (var o in memePanels.Select((panel, index) => new { panel, index }))
            {
                o.panel.StaticMemeUrl = memeImages[o.index].ImageUrl;
            }

            await _memePanelRepository.SaveAsync();
        }
    }
}
