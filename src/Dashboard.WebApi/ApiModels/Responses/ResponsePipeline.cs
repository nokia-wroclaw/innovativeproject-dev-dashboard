using Dashboard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.WebApi.ApiModels.Responses
{
    public class ResponsePipeline
    {
        public int Id { get; set; }
        public string ProjectId { get; set; }
        public string Ref { get; set; }
        public Status Status { get; set; }
        public string CommitTitle { get; set; }
        public string CommiterName { get; set; }
        public string CommiterEmail { get; set; }
        public IEnumerable<ResponseStage> Stages { get; set; }

        public ResponsePipeline()
        {

        }

        public ResponsePipeline(Pipeline pipe)
        {
            this.Id = pipe.Id;
            this.ProjectId = pipe.ProjectId;
            this.Ref = pipe.Ref;
            this.Status = pipe.Status;
            this.CommitTitle = pipe.CommitTitle;
            this.CommiterName = pipe.CommiterName;
            this.CommiterEmail = pipe.CommiterEmail;
            this.Stages = pipe.Stages.Select(p => new ResponseStage(p));
        }
    }
}
