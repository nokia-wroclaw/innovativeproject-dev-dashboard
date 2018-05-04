using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.Validators
{
    public class CreateProjectValidator : ProjectValidator
    {
        public CreateProjectValidator()
        {
            ValidateTitle();
            ValidateApiHostUrl();
            ValidateApiProjectId();
            ValidateApiAuthenticationToken();
            ValidateDataProviderName();
            ValidateCiDataUpdateCronExpression();
        }
    }
}
