using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Dashboard.Data.Repositories;

namespace Dashboard.Application.AutofacModules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(PanelRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsSelf()
                .AsImplementedInterfaces();
        }
    }
}
