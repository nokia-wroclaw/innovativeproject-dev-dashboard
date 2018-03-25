using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Core.Entities;

namespace Dashboard.WebApi.ApiModels.Requests
{
    public class UpdatePanelPositions
    {
        [Required]
        public IEnumerable<UpdatedPanelPosition> UpdatedPanelPositions { get; set; }
    }

    public class UpdatedPanelPosition
    {
        [Required]
        public int PanelId { get; set; }

        [Required]
        public PanelPosition Position { get; set; }
    }
}
