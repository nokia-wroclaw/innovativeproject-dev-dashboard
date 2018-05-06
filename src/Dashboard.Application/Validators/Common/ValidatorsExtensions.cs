using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Dashboard.Application.Validators.Common
{
    public static class ValidatorsExtensions
    {
        public static IRuleBuilderOptions<T, string> CronExpression<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CronExpressionValidator());
        }
    }
}
