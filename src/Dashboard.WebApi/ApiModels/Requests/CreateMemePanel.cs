using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.WebApi.ApiModels.Requests
{
    public class CreateMemePanel : CreatePanel
    {
        [Required]
        public string MemeApiToken { get; set; }
    }
}
