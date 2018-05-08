using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Application.Interfaces.Services;
using FluentValidation;
using FluentValidation.Results;

namespace Dashboard.Application.Services
{
    public class FluentValidationService : IValidationService
    {
        private readonly IValidatorFactory _validatorFactory;
        private readonly IServiceProvider _serviceProvider;

        public FluentValidationService(IValidatorFactory validatorFactory, IServiceProvider _serviceProvider)
        {
            this._validatorFactory = validatorFactory;
            this._serviceProvider = _serviceProvider;
        }

        public Task<ValidationResult> ValidateAsync<T>(T entity) where T : class
        {
            var validator = this._validatorFactory.GetValidator(entity.GetType());
            return validator.ValidateAsync(entity);
        }

        public Task<ValidationResult> ValidateAsync<TValidator, T>(T entity) where TValidator : IValidator<T> where T : class
        {
            var validator = (TValidator)_serviceProvider.GetService(typeof(TValidator));
            return validator.ValidateAsync(entity);
        }
    }
}
