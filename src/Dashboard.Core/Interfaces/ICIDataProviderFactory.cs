using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Core.Interfaces
{
    public interface ICIDataProviderFactory
    {
        IEnumerable<ICIDataProvider> GetSupportedProviders { get; }

        ICIDataProvider CreateForProviderName(string name);
    }
}
