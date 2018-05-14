using Dashboard.Core.Interfaces;
using Dashboard.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dashboard.Core.Entities
{
    public abstract class Panel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual PanelPosition Position { get; set; }

        public abstract string Discriminator { get; }

        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public bool ShouldSerializeProject()
        {
            return false;
        }
    }

    public class MemePanel : Panel
    {
        public override string Discriminator => nameof(MemePanel);

        public string StaticMemeUrl { get; set; }
    }

    public class StaticBranchPanel : Panel, IPanelPipelines
    {
        public override string Discriminator => nameof(StaticBranchPanel);

        public string StaticBranchName { get; set; }

        public async Task<IEnumerable<Pipeline>> GetPipelinesDTOForPanel(IProjectRepository projectRepository)
        {
            return new List<Pipeline> { Project.Pipelines.FirstOrDefault(p => p.Ref.Equals(StaticBranchName)) };
        }
    }

    public class DynamicPipelinesPanel : Panel, IPanelPipelines
    {
        public override string Discriminator => nameof(DynamicPipelinesPanel);

        public int HowManyLastPipelinesToRead { get; set; }
        public string PanelRegex { get; set; }

        public async Task<IEnumerable<Pipeline>> GetPipelinesDTOForPanel(IProjectRepository projectRepository)
        {
            int projID = ProjectId ?? -1;
            if (projID == -1) return new List<Pipeline>();

            return Project.Pipelines.Where(p => Regex.IsMatch(p.Ref, PanelRegex)).Select(p => p).Take(HowManyLastPipelinesToRead);
        }
    }

    public class PanelPosition
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
