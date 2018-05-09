using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FluentValidation.Validators;
using NCrontab;

namespace Dashboard.Application.Validators.Common
{
    public class RegexPatternValidator : PropertyValidator, IRegexPatternValidator
    {

        public RegexPatternValidator() : base("{PropertyName} is not a valid regex pattern.")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var pattern = (string)context.PropertyValue;

            if (string.IsNullOrEmpty(pattern)) return false;

            try
            {
                Regex.Match("", pattern);
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }
    }

    public interface IRegexPatternValidator : IPropertyValidator
    {
    }
}
