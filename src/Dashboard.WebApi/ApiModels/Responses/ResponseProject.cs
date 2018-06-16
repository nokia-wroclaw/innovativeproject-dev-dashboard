using Dashboard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.WebApi.ApiModels.Responses
{
    public class ResponseProject
    {
        public int Id { get; set; }
        public string ProjectTitle { get; set; }
        public string ApiHostUrl { get; set; }
        public string ApiProjectId { get; set; }
        public string ApiAuthenticationToken { get; set; }
        public string DataProviderName { get; set; }
        public string CiDataUpdateCronExpression { get; set; }

        public int PipelinesNumber { get; set; }
        public virtual ICollection<ResponsePipeline> Pipelines { get; set; }

        public ResponseProject()
        {

        }

        public ResponseProject(Project project)
        {
            this.Id = project.Id;
            this.ProjectTitle = project.ProjectTitle;
            this.ApiHostUrl = project.ApiHostUrl;
            this.ApiProjectId = project.ApiProjectId;
            this.ApiAuthenticationToken = project.ApiAuthenticationToken;
            this.DataProviderName = project.DataProviderName;
            this.CiDataUpdateCronExpression = project.CiDataUpdateCronExpression;

            this.PipelinesNumber = project.PipelinesNumber;
            this.Pipelines = project.Pipelines.Select(p => new ResponsePipeline(p)).ToList();
        }
    }
}
