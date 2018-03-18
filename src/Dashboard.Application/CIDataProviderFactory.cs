using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dashboard.Core.Interfaces;

namespace Dashboard.Application
{
    public class CIDataProviderFactory : ICIDataProviderFactory
    {
        private IEnumerable<ICIDataProvider> AllProviders { get; }

        /// <summary>
        /// Id doesnt actualy create providers,  its more like filter
        /// </summary>
        /// <param name="allProviders">All registered providers from IoC container</param>
        public CIDataProviderFactory(IEnumerable<ICIDataProvider> allProviders)
        {
            AllProviders = allProviders;
        }

        public IEnumerable<ICIDataProvider> GetSupportedProviders => AllProviders;

        public ICIDataProvider CreateForProviderName(string name)
        {
            return AllProviders.FirstOrDefault(p => p.Name == name);
        }
    }
}
