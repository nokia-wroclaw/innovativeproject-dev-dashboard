using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.WebApi.ApiModels.Requests
{
    public class UpdatePanelPosition
    {
        [Required]
        public int Column { get; set; }
        [Required]
        public int Row { get; set; }
        [Required]
        public int Width { get; set; }
        [Required]
        public int Height { get; set; }
    }
}
