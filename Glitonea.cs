namespace Glitonea;

using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using global::Glitonea.Extensibility;
using global::Glitonea.Mvvm;
using global::Glitonea.Mvvm.Modules;

public class Glitonea : ResourceDictionary
{
    private static bool _initialized;

    private static IContainer? Container { get; set; }
    private static ContainerBuilder? ContainerBuilder { get; set; }

    public Glitonea()
    {
        AvaloniaXamlLoader.Load(this);
    }

    internal static void Initialize(Assembly[] sourceAssemblies, IEnumerable<ContainerBuilderNotificationDelegate> onBuildingSubscribers)
    {
        if (_initialized)
            throw new InvalidOperationException("Attempt to initialize Glitonea twice.");

        ContainerBuilder = new ContainerBuilder();

        foreach (var assembly in sourceAssemblies)
        {
            ContainerBuilder.RegisterModule(new ViewModelModule(assembly));
            ContainerBuilder.RegisterModule(new ServiceModule(assembly));
        }
            
        NotifyContainerBuildingSubscribers(ContainerBuilder, onBuildingSubscribers);
        Container = ContainerBuilder.Build();
            
        ViewModelResolver.Instance.Initialize(Container);
        ServiceResolver.Instance.Initialize(Container);

        _initialized = true;
    }

    private static void NotifyContainerBuildingSubscribers(
        ContainerBuilder containerBuilder, 
        IEnumerable<ContainerBuilderNotificationDelegate> onBuildingSubscribers)
    {
        foreach (var subscriber in onBuildingSubscribers) 
            subscriber(containerBuilder);
    }
}