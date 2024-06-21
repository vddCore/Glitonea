namespace Glitonea.Mvvm;

using System;
using Autofac;

public class ServiceResolver
{
    private IContainer? _container;
    private static ServiceResolver? _instance;

    public static ServiceResolver Instance => _instance ?? (_instance = new ServiceResolver());

    public void Initialize(IContainer container)
    {
        _container = container;
    }

    public T Resolve<T>() where T: IService
    {
        if (_container == null)
            throw new InvalidOperationException("Service resolver not initialized yet.");

        return _container.Resolve<T>();
    }

    public object? Resolve(Type t)
        => _container?.Resolve(t);
}