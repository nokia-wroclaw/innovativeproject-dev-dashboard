using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Core.Entities;

namespace Dashboard.WebApi.ApiModels.Requests
{
    public class CreatePanel
    {
        //Validation?
        public IEnumerable<BranchName> StaticBranchNames { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public PanelType Type { get; set; }
        [Required]
        public PanelPosition Position { get; set; } = new PanelPosition();
        [Required]
        public string Data { get; set; }
        [Required]
        public int ProjectId { get; set; }
    }
}
