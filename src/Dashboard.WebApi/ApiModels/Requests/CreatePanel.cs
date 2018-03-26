using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dashboard.Core.Entities;
using Dashboard.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.WebApi.ApiModels.Requests
{
    public abstract class CreatePanel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public PanelPosition Position { get; set; }
        [Required]
        public int ProjectId { get; set; }
    }
}
