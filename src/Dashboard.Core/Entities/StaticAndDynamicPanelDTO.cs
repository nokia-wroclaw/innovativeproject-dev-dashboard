using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Core.Entities
{
    public class StaticAndDynamicPanelDTO
    {
        public IEnumerable<Pipeline> Pipelines { get; set; }
    }
}
