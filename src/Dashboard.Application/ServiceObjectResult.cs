using FluentValidation.Results;

namespace Dashboard.Application
{
    public class ServiceObjectResult<T>
    {
        public static ServiceObjectResult<T> Ok(T data) => new ServiceObjectResult<T>(data);
        public static ServiceObjectResult<T> Error(ValidationResult validationResult) => new ServiceObjectResult<T>(validationResult);

        public T Data { get; }
        public ValidationResult ValidationResult { get; }

        public bool IsSuccess => ValidationResult == null || ValidationResult.IsValid;

        private ServiceObjectResult(T data)
        {
            Data = data;
        }

        private ServiceObjectResult(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}
