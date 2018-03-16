using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Core.Entities;

namespace Dashboard.WebApi.ApiModels.Requests
{
    public class CreateProjectTile
    {
        [Required]
        public string ApiHostUrl { get; set; }

        [Required]
        public string ApiProjectId { get; set; }

        [Required]
        public string ApiAuthenticationToken { get; set; }

        [Required]
        public string DataProviderName { get; set; }

        [Required]
        public FrontConfig FrontConfig { get; set; }
    }
}
