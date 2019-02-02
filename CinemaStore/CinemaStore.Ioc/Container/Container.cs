using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CinemaStore.Ioc.Container
{
    public class Container
    {
        public static void RegisterIoC(Assembly assembly)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(assembly);

            var register = new RegisterTypes();
            register.InitDependencies(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
