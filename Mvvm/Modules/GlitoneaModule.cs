using System.Reflection;
using Module = Autofac.Module;

namespace Glitonea.Mvvm
{
    internal abstract class GlitoneaModule : Module
    {
        protected Assembly CallingAssembly { get; }
        
        internal GlitoneaModule(Assembly callingAssembly)
        {
            CallingAssembly = callingAssembly;
        }
    }
}