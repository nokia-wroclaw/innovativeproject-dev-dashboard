﻿using Dashboard.Core.Interfaces;
using Dashboard.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dashboard.Core.Entities
{
    public abstract class Panel : IPanelPipelinesDTO
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

        public virtual async Task<StaticAndDynamicPanelDTO> GetPipelinesDTOForPanel(int panelID, IProjectRepository projectRepository)
        {
            return await Task.Run(() => new StaticAndDynamicPanelDTO());
        }
    }

    public class MemePanel : Panel
    {
        public override string Discriminator => nameof(MemePanel);

        public string MemeApiToken { get; set; }
    }

    public class StaticBranchPanel : Panel
    {
        public override string Discriminator => nameof(StaticBranchPanel);

        public string StaticBranchName { get; set; }

        public override async Task<StaticAndDynamicPanelDTO> GetPipelinesDTOForPanel(int panelID, IProjectRepository projectRepository)
        {
            int projID = ProjectId ?? throw new ArgumentException($"DB does NOT contain panel with ID={panelID}");
            var projectPipelines = (await projectRepository.GetByIdAsync(projID)).Pipelines;
            return new StaticAndDynamicPanelDTO()
            {
                Pipelines = new List<Pipeline> { projectPipelines.LastOrDefault(p => p.Ref.Equals(StaticBranchName)) }
            };
        }
    }

    public class DynamicPipelinesPanel : Panel
    {
        public override string Discriminator => nameof(DynamicPipelinesPanel);

        public int HowManyLastPipelinesToRead { get; set; }
        public string PanelRegex { get; set; }

        public override async Task<StaticAndDynamicPanelDTO> GetPipelinesDTOForPanel(int panelID, IProjectRepository projectRepository)
        {
            int projID = ProjectId ?? throw new ArgumentException($"DB does NOT contain panel with ID={panelID}");
            var projectPipelines = (await projectRepository.GetByIdAsync(projID)).Pipelines;
            return new StaticAndDynamicPanelDTO()
            {
                Pipelines = projectPipelines.Where(p => Regex.IsMatch(p.Ref, PanelRegex)).Select(p => p).TakeLast(HowManyLastPipelinesToRead)
            };
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
