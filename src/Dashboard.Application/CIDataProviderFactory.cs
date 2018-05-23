using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dashboard.Core.Interfaces;

namespace Dashboard.Application
{
    public class CiDataProviderFactory : ICiDataProviderFactory
    {
        private IEnumerable<ICiDataProvider> AllProviders { get; }

        /// <summary>
        /// It doesnt actually create providers,  its more like filter
        /// </summary>
        /// <param name="allProviders">All registered providers from IoC container</param>
        public CiDataProviderFactory(IEnumerable<ICiDataProvider> allProviders)
        {
            AllProviders = allProviders;
        }

        public IEnumerable<ICiDataProvider> GetSupportedProviders => AllProviders;

        public ICiDataProvider CreateForProviderName(string name)
        {
            return AllProviders.FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
