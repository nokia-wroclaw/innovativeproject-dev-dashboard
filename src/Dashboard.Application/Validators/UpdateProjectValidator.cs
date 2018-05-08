using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.Validators
{
    public class UpdateProjectValidator : ProjectValidator
    {
        public UpdateProjectValidator()
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
