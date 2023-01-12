using System;
using System.Reflection;
using Autofac;
using Glitonea.Mvvm;
using Microsoft.CodeAnalysis;

namespace Glitonea
{
    public static class GlitoneaCore
    {
        private static bool _initialized;
        
        private static IContainer Container { get; set; }
        private static ContainerBuilder ContainerBuilder { get; set; }
        
        public static void Initialize()
        {
            if (_initialized)
                throw new InvalidOperationException("Attempt to initialize Glitonea twice.");

            ContainerBuilder = new ContainerBuilder();
            ContainerBuilder.RegisterModule(new ViewModelModule(Assembly.GetCallingAssembly()));
            Container = ContainerBuilder.Build();

            ViewModelResolver.Instance.Initialize(Container);

            _initialized = true;
        }
    }
}