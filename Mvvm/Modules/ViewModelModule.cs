namespace Glitonea.Mvvm.Modules;

using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using global::Glitonea.Mvvm.Messaging;

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
            .OnActivated(InitializeViewModel)
            .InstancePerDependency();

        builder.RegisterAssemblyTypes(CallingAssembly)
            .Where(type => type.IsAssignableTo<SingleInstanceViewModelBase>())
            .AsSelf()
            .OnActivated(InitializeViewModel)
            .SingleInstance();
    }

    private void InitializeViewModel(object e)
    {
        AutoRegisterMessageHandlers((ActivatedEventArgs<object>)e);
    }

    private void AutoRegisterMessageHandlers(ActivatedEventArgs<object> e)
    {
        var instance = e.Instance;
        var type = instance.GetType();

        if (type.GetCustomAttribute<DisableMessageHandlerAutoRegistrationAttribute>() != null)
            return;
                
        var messageHandlerMethods = type.GetMethods(
            BindingFlags.Instance 
            | BindingFlags.Public
            | BindingFlags.NonPublic
        ).Where(m => m.GetCustomAttribute<MessageHandlerAttribute>() != null);

        foreach (var messageHandlerMethod in messageHandlerMethods)
        {
            var parameters = messageHandlerMethod.GetParameters();
                    
            if (parameters.Length != 1)
                continue;
                    
            var parameterType = parameters[0].ParameterType;

            if (!parameterType.IsAssignableTo<Message>())
                continue;

            var subscribeGenericMethod = typeof(Message)
                .GetMethod(
                    nameof(Message.Subscribe),
                    BindingFlags.Static 
                    | BindingFlags.Public 
                    | BindingFlags.NonPublic
                )!.MakeGenericMethod(parameterType);
                    
            var actionType = typeof(Action<>).MakeGenericType(parameterType);
            subscribeGenericMethod.Invoke(
                null, 
                [instance, messageHandlerMethod.CreateDelegate(actionType, instance)]
            );
        }
    }
}