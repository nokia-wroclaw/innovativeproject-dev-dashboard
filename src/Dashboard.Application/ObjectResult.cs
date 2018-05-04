using FluentValidation.Results;

namespace Dashboard.Application
{
    public class ObjectResult<T>
    {
        public static ObjectResult<T> Ok(T data) => new ObjectResult<T>(data);
        public static ObjectResult<T> Error(ValidationResult validationResult) => new ObjectResult<T>(validationResult);

        public T Data { get; }
        public ValidationResult ValidationResult { get; }

        public bool IsSuccess => ValidationResult == null || ValidationResult.IsValid;

        private ObjectResult(T data)
        {
            Data = data;
        }

        private ObjectResult(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}
