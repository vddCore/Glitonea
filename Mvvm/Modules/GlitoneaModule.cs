namespace Glitonea.Mvvm.Modules;

using System.Reflection;
using Module = Autofac.Module;

internal abstract class GlitoneaModule : Module
{
    protected Assembly CallingAssembly { get; }
        
    internal GlitoneaModule(Assembly callingAssembly)
    {
        CallingAssembly = callingAssembly;
    }
}