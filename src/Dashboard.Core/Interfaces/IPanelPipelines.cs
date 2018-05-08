using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.Repositories;

namespace Dashboard.Core.Interfaces
{
    public interface IPanelPipelines
    {
        Task<StaticAndDynamicPanel> GetPipelinesDTOForPanel(int panelID, IProjectRepository projectRepository);
    }
}
