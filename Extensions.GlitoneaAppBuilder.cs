namespace Glitonea;

using System.Collections.Generic;
using System.Reflection;
using Avalonia;
using global::Glitonea.Extensibility;

public static partial class Extensions
{
    public static AppBuilder UseGlitoneaFramework(this AppBuilder appBuilder, params Assembly[] sourceAssemblies)
    {
        var subscribers = new List<ContainerBuilderNotificationDelegate>();
        
        if (appBuilder.Instance is IContainerBuildingSubscriber subscriber) 
            subscribers.Add(subscriber.OnBuildingIoC);
        
        Glitonea.Initialize(sourceAssemblies, subscribers);
        return appBuilder;
    }
}