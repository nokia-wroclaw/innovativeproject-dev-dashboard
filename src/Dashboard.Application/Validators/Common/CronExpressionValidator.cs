using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using FluentValidation;
using FluentValidation.Resources;
using FluentValidation.Validators;
using NCrontab;

namespace Dashboard.Application.Validators.Common
{
    public class CronExpressionValidator : PropertyValidator, ICronExpressionValidator
    {

        public CronExpressionValidator() : base("{PropertyName} is not a valid Cron Expression.")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var expresion = (string) context.PropertyValue;

            var isValid = CrontabSchedule.TryParse(expresion) != null;
            return isValid;
        }
    }

    public interface ICronExpressionValidator : IPropertyValidator
    {
    }
}
