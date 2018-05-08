using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.Validators
{
    public class FullPanelPositionValidator : PanelPositionValidator
    {
        public FullPanelPositionValidator()
        {
            base.ValidateColumn();
            base.ValidateRow();

            base.ValidateHeight();
            base.ValidateWidth();
        }
    }
}
