using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace Dashboard.Application.Interfaces.Services
{
    public interface IValidationService
    {
        Task<ValidationResult> ValidateAsync<T>(T entity) where T : class;
        Task<ValidationResult> ValidateAsync<TValidator, T>(T entity)
            where T : class
            where TValidator : IValidator<T>;
    }
}
