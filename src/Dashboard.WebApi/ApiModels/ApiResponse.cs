using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dashboard.Application;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.WebApi.ApiModels
{
    public class ApiResponse
    {
        public static OkObjectResult Ok(object data) => new OkObjectResult(data);
        public static BadRequestObjectResult Error(ValidationResult validationResult) => new BadRequestObjectResult(new ApiResponse(validationResult));

        public static ObjectResult FromServiceResult<T>(ServiceObjectResult<T> result) => result.IsSuccess ? (ObjectResult) Ok(result.Data) : Error(result.ValidationResult);
        

        public bool Success { get; }
        public HttpStatusCode Status { get; }
        public IEnumerable<NormalizedError> Errors { get; set; }

        public ApiResponse(ValidationResult validationResult)
        {
            Status = HttpStatusCode.BadRequest;
            Success = validationResult.IsValid;
            Errors = validationResult.Errors.Select(f => new NormalizedError(f));
        }
    }

    public class NormalizedError
    {
        public string ErrorMessage { get; }
        public string ErrorCode { get; }

        public NormalizedError(ValidationFailure failure)
        {
            ErrorMessage = failure.ErrorMessage;
            ErrorCode = failure.ErrorCode;
        }

    }
}
