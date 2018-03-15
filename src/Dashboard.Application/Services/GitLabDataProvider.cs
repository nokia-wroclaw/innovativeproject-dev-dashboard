using System.Collections.Generic;
using System.Threading.Tasks;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.Repositories;

namespace Dashboard.Application.Services
{
    public class GitLabDataProvider : ICIDataProvider
    {
        private readonly IPipelineRepository _pipelineRepository;

        public GitLabDataProvider(IPipelineRepository pipelineRepository)
        {
            _pipelineRepository = pipelineRepository;
        }

        public Task<Pipeline> GetMasterAsync()
        {
            return _pipelineRepository.FindOneByAsync(pipe => pipe.RefBranch.Equals("master"));
        }
    }
}
