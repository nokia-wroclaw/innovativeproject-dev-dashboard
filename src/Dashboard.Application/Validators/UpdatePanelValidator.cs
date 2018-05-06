using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.Validators
{
    public class UpdatePanelValidator : PanelValidator
    {
        public UpdatePanelValidator(FullPanelPositionValidator panelPositionValidator)
        {
            base.ValidateTitle();
            base.ValidatePanelPosition(panelPositionValidator);
            ValidateProject();
        }

        private void ValidateProject()
        {
            
        }
    }
}
