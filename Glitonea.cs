namespace Glitonea;

using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

using global::Glitonea.Mvvm;
using global::Glitonea.Mvvm.Modules;

public class Glitonea : ResourceDictionary
{
    private static bool _initialized;

    private static List<Action<ContainerBuilder>> _onContainerBuildingInvocationList = [];
        
    private static IContainer? Container { get; set; }
    private static ContainerBuilder? ContainerBuilder { get; set; }

    public Glitonea()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public static void RegisterContainerBuildingNotification(Action<ContainerBuilder> action)
    {
        if (_onContainerBuildingInvocationList.Contains(action))
            return;
            
        _onContainerBuildingInvocationList.Add(action);
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

        foreach (var subscriber in _onContainerBuildingInvocationList)
        {
            subscriber.Invoke(ContainerBuilder);
        }
            
        Container = ContainerBuilder.Build();
            
        ViewModelResolver.Instance.Initialize(Container);
        ServiceResolver.Instance.Initialize(Container);

        _initialized = true;
    }
}