using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Application.Services;
using Dashboard.Application.Validators;
using FluentValidation;

namespace Dashboard.Application.AutofacModules
{
    public class ValidaionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Validation
            builder.RegisterType<FluentValidationService>().As<IValidationService>();
            builder.RegisterType<AutofacValidatorFactory>().As<IValidatorFactory>();

            //Search all Validators in PanelValidator assembly
            builder.RegisterAssemblyTypes(typeof(PanelValidator).Assembly)
                .Where(t => t.Name.EndsWith("Validator"))
                .AsSelf()
                .AsImplementedInterfaces();
        }
    }
}
