using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Glitonea.Mvvm
{
    internal class ViewModelModule : Module
    {
        private readonly Assembly _callingAssembly;
        
        internal ViewModelModule(Assembly callingAssembly)
        {
            _callingAssembly = callingAssembly;
        }
        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(_callingAssembly)
                .Where(type => type.IsAssignableTo<ViewModelBase>())
                .AsSelf()
                .InstancePerDependency();
        }
    }
}