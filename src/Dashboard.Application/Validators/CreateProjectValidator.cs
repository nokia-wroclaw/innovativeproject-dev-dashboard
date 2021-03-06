﻿using System;
using System.Collections.Generic;
using System.Text;
using Dashboard.Core.Interfaces;

namespace Dashboard.Application.Validators
{
    public class CreateProjectValidator : ProjectValidator
    {
        public CreateProjectValidator(ICiDataProviderFactory factory)
        {
            ValidateTitle();
            ValidateApiHostUrl();
            ValidateApiProjectId();
            ValidateApiAuthenticationToken();
            ValidateDataProviderName();
            ValidateCiDataUpdateCronExpression();
            ValidateApiCredentials(factory);
        }
    }
}
