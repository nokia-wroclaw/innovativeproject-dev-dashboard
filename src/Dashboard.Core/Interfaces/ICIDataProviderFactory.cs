using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Core.Interfaces
{
    public interface ICiDataProviderFactory
    {
        IEnumerable<ICiDataProvider> GetSupportedProviders { get; }

        ICiDataProvider CreateForProviderName(string name);
    }
}
