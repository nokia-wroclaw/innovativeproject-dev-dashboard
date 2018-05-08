using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.Validators
{
    public class CreatePanelValidator : PanelValidator
    {
        public CreatePanelValidator(FullPanelPositionValidator panelPositionValidator)
        {
            base.ValidateTitle();
            base.ValidatePanelPosition(panelPositionValidator);
        }
    }
}
