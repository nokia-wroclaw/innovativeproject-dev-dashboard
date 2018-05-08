using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Application.Services;

namespace Dashboard.Application.AutofacModules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(PanelService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsSelf()
                .AsImplementedInterfaces();
        }
    }
}
