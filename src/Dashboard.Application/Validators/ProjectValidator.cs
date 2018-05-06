using Dashboard.Application.Validators.Common;
using Dashboard.Core.Entities;
using FluentValidation;

namespace Dashboard.Application.Validators
{
    public abstract class ProjectValidator : AbstractValidator<Project>
    {
        protected void ValidateTitle()
        {
            RuleFor(p => p.ProjectTitle)
                .NotEmpty();
        }
        protected void ValidateApiHostUrl()
        {
            RuleFor(p => p.ApiHostUrl)
                .NotEmpty();
        }
        protected void ValidateApiProjectId()
        {
            RuleFor(p => p.ApiProjectId)
                .NotEmpty();
        }
        protected void ValidateApiAuthenticationToken()
        {
            RuleFor(p => p.ApiAuthenticationToken)
                .NotEmpty();
        }
        protected void ValidateDataProviderName()
        {
            RuleFor(p => p.DataProviderName)
                .NotEmpty();
        }
        protected void ValidateCiDataUpdateCronExpression()
        {
            RuleFor(p => p.CiDataUpdateCronExpression)
                .NotEmpty()
                .CronExpression();
        }
    }
}
