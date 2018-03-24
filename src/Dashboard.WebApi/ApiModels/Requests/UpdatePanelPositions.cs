using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public int Column { get; set; }
        [Required]
        public int Row { get; set; }
        [Required]
        public int Width { get; set; }
        [Required]
        public int Hight { get; set; }
    }
}
