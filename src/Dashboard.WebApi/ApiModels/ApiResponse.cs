using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Dashboard.WebApi.ApiModels
{
    public class ApiResponse
    {
        public static ApiResponse Ok => new ApiResponse(HttpStatusCode.OK, true);
        public static ApiResponse NotFound => new ApiResponse(HttpStatusCode.NotFound, false);

        public bool Success { get; }
        public HttpStatusCode Status { get; }
        public object Result { get; set; }

        public ApiResponse(HttpStatusCode code, bool success)
        {
            Status = code;
            Success = success;
        }
    }
}
