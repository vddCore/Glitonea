using System;
using System.Reflection;
using Autofac;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Glitonea.Mvvm;
using Glitonea.Mvvm.Modules;

namespace Glitonea
{
    public class Glitonea : ResourceDictionary
    {
        private static bool _initialized;
        
        private static IContainer? Container { get; set; }
        private static ContainerBuilder? ContainerBuilder { get; set; }

        public Glitonea()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        internal static void Initialize(Assembly[] sourceAssemblies)
        {
            if (_initialized)
                throw new InvalidOperationException("Attempt to initialize Glitonea twice.");


            ContainerBuilder = new ContainerBuilder();

            foreach (var assembly in sourceAssemblies)
            {
                ContainerBuilder.RegisterModule(new ViewModelModule(assembly));
                ContainerBuilder.RegisterModule(new ServiceModule(assembly));
            }

            Container = ContainerBuilder.Build();
            ViewModelResolver.Instance.Initialize(Container);
            ServiceResolver.Instance.Initialize(Container);

            _initialized = true;
        }
    }
}