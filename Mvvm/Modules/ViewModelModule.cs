namespace Glitonea.Mvvm.Modules;

using System.Reflection;
using Autofac;

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

        builder.RegisterAssemblyTypes(CallingAssembly)
            .Where(type => type.IsAssignableTo<SingleInstanceViewModelBase>())
            .AsSelf()
            .SingleInstance();
    }
}