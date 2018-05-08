using System;
using System.Collections.Generic;
using System.Text;
using Dashboard.Core.Entities;
using FluentValidation;

namespace Dashboard.Application.Validators
{
    public class PanelPositionValidator : AbstractValidator<PanelPosition>
    {
        public void ValidateHeight()
        {
            RuleFor(p => p.Height)
                .GreaterThanOrEqualTo(0);
        }

        public void ValidateWidth()
        {
            RuleFor(p => p.Width)
                .GreaterThanOrEqualTo(0);
        }

        public void ValidateColumn()
        {
            RuleFor(p => p.Column)
                .GreaterThanOrEqualTo(0);
        }

        public void ValidateRow()
        {
            RuleFor(p => p.Row)
                .GreaterThanOrEqualTo(0);
        }
    }
}
