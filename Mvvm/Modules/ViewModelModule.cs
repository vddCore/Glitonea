using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Glitonea.Mvvm
{
    internal class ViewModelModule : GlitoneaModule
    {
        public ViewModelModule(Assembly callingAssembly) 
            : base(callingAssembly)
        {
        }
        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(CallingAssembly)
                .Where(type => type.IsAssignableTo<ViewModelBase>())
                .AsSelf()
                .InstancePerDependency();
        }
    }
}