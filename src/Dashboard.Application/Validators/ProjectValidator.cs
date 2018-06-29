using System;
using System.Threading.Tasks;
using Dashboard.Application.Validators.Common;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces;
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

        protected void ValidateApiCredentials(ICiDataProviderFactory factory)
        {
            RuleFor(p => p.ApiHostUrl)
                .MustAsync((project, hostUrl, ct) => TestApiCredentials(project, hostUrl, factory))
                .WithMessage("Can't access secured API endpoint with these credentials");
        }

        private async Task<bool> TestApiCredentials(Project project, string apiHostUrl, ICiDataProviderFactory factory)
        {
            var provider = factory.CreateForProviderName(project.DataProviderName);

            try
            {
                var result = await provider.TestApiCredentials(project.ApiHostUrl, project.ApiAuthenticationToken);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
