using Autofac;
using LostHobbit.Interfaces;
using LostHobbit.Interfaces.Events;
using LostHobbit.Services;
using LostHobbit.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostHobbit.AutoFac
{
    public class DefaultModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TimedEventRunner>().As<ITimedEventRunner>().InstancePerLifetimeScope();
            builder.RegisterType<UserActionEventRunner>().As<IUserActionEventRunner>().InstancePerLifetimeScope();
            builder.RegisterType<Security>().As<ISecurity>().InstancePerLifetimeScope();
            builder.RegisterType<Email>().As<IEmail>().InstancePerLifetimeScope();
        }
    }
}
