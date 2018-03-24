using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Core.Entities;

namespace Dashboard.WebApi.ApiModels.Responses
{
    public class PanelViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public PanelPosition Position { get; set; } = new PanelPosition();
        /// <summary>
        /// If panel may contain more than one card in itself
        /// </summary>
        public bool IsDynamic { get; }
        public string Discriminator { get; }
        public int ProjectId { get; set; }

        public string MemeApiToken { get; set; }
        public IEnumerable<string> StaticBranchNames { get; set; }
        public int HowManyLastPipelinesToRead { get; set; }

        public PanelViewModel(Panel panel)
        {
            Id = panel.Id;
            Title = panel.Title;
            Position = panel.Position;

            IsDynamic = panel.IsDynamic;
            Discriminator = panel.Discriminator;
        }

        public PanelViewModel(MemePanel panel) : this((Panel)panel)
        {
            MemeApiToken = panel.MemeApiToken;
        }

        public PanelViewModel(StaticBranchPanel panel) : this((Panel) panel)
        {
            StaticBranchNames = panel.StaticBranchNames.Select(b => b.Name);
        }

        public PanelViewModel(DynamicPipelinesPanel panel) : this((Panel)panel)
        {
            HowManyLastPipelinesToRead = panel.HowManyLastPipelinesToRead;
        }
    }
}
