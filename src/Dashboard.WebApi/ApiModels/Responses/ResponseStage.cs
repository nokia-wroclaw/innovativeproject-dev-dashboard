using Dashboard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.WebApi.ApiModels.Responses
{
    public class ResponseStage
    {
        public int Id { get; set; }
        public string StageName { get; set; }
        public Status StageStatus { get; set; }
        public int Successed { get; set; }
        public int Total { get; set; }

        public ResponseStage()
        {

        }

        public ResponseStage(Stage stage)
        {
            this.Id = stage.Id;
            this.StageName = stage.StageName;
            this.StageStatus = stage.StageStatus;
            this.Successed = stage.Jobs.Count(p => p.Status == Status.Success);
            this.Total = stage.Jobs.Count();
        }
    }
}
