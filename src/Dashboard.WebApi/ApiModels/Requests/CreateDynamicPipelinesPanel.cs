using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.WebApi.ApiModels.Requests
{
    public class CreateDynamicPipelinesPanel : CreatePanel
    {
        public int HowManyLastPipelinesToRead { get; set; }
    }
}
