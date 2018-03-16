using System.ComponentModel.DataAnnotations;
using Dashboard.Core.Entities;

namespace Dashboard.WebApi.ApiModels.Requests
{
    public class UpdateProjectTile
    {
        [Required]
        public int Id { get; set; }

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
