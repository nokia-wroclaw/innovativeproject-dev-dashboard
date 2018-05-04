using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Dashboard.WebApi.ApiModels
{
    public class ApiResponse
    {
        public static ApiResponse Ok => new ApiResponse(HttpStatusCode.OK, true);
        public static ApiResponse Error(ValidationResult validationResult) => new ApiResponse(validationResult);

        public bool Success { get; }
        public HttpStatusCode Status { get; }
        public object Result { get; set; }

        public ApiResponse(HttpStatusCode code, bool success)
        {
            Status = code;
            Success = success;
        }

        public ApiResponse(ValidationResult validationResult)
        {
            Status = HttpStatusCode.BadRequest;
            Success = validationResult.IsValid;
            Result = validationResult.Errors;
        }
    }
}
