using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Core.Entities;

namespace Dashboard.WebApi.ApiModels.Responses
{
    public class PanelResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public PanelType Type { get; set; }
        public PanelPosition Position { get; set; } = new PanelPosition();
        public string Data { get; set; }

        public int ProjectId { get; set; }
        public IEnumerable<BranchName> StaticBranchNames { get; set; } = new List<BranchName>();

        public PanelResponse()
        {
        }

        public PanelResponse(Panel panel)
        {
            Id = panel.Id;
            Position = panel.Position;
            StaticBranchNames = panel.StaticBranchNames;
            Data = panel.Data;
            Type = panel.Type;
            Title = panel.Title;
            ProjectId = panel.Project.Id;
        }
    }
}
