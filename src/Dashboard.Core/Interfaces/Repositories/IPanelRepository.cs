﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Core.Entities;

namespace Dashboard.Core.Interfaces.Repositories
{
    public interface IPanelRepository : IEfRepository<Panel>
    {
    }

    public interface IStaticBranchPanelRepository : IEfRepository<StaticBranchPanel>
    {
        Task<IEnumerable<string>> GetBranchNamesFromStaticPanelsForProject(int projectId);
    }

    public interface IMemePanelRepository : IEfRepository<MemePanel>
    {
    }

    public interface IDynamicPipelinesPanelRepository : IEfRepository<DynamicPipelinesPanel>
    {
    }
}
