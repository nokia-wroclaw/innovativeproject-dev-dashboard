using System.ComponentModel.DataAnnotations;

namespace Dashboard.WebApi.ApiModels.Requests
{
    public class CreateProject
    {
        [Required]
        public string ProjectTitle { get; set; }
        [Required]
        public string ApiHostUrl { get; set; }
        [Required]
        public string ApiProjectId { get; set; }
        [Required]
        public string ApiAuthenticationToken { get; set; }
        [Required]
        public string DataProviderName { get; set; }
        [Required]
        public string CiDataUpdateCronExpression { get; set; }
        public int PipelinesNumber { get; set; }
    }
}
