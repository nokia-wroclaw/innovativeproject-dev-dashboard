using System;
using System.Collections.Generic;
using System.Text;
using Dashboard.Application.Validators.Common;
using Dashboard.Core.Entities;
using FluentValidation;

namespace Dashboard.Application.Validators
{
    public abstract class PanelValidator : AbstractValidator<Panel>
    {
        public void ValidateTitle()
        {
            RuleFor(p => p.Title)
                .NotEmpty();
        }

        public void ValidatePanelPosition(IValidator<PanelPosition> panelPositionValidator)
        {
            RuleFor(p => p.Position)
                .SetValidator(panelPositionValidator);
        }

        public void ValidatePanelRegex()
        {
            When(model => model.GetType() == typeof(DynamicPipelinesPanel), () =>
            {
                RuleFor(model => ((DynamicPipelinesPanel) model).PanelRegex)
                    .Regex();
            });
        }
    }
}
