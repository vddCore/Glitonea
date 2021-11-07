using System.Reflection;
using Autofac;

namespace Glitonea.Mvvm
{
    internal class ServiceModule : GlitoneaModule 
    {
        public ServiceModule(Assembly callingAssembly) 
            : base(callingAssembly)
        {
        }
        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(CallingAssembly)
                .Where(type => type.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}