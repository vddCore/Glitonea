﻿namespace Glitonea.Mvvm;

using System;
using Autofac;

public class ViewModelResolver
{
    private IContainer? _container;
    private static ViewModelResolver? _instance;

    public static ViewModelResolver Instance => _instance ?? (_instance = new ViewModelResolver());

    private ViewModelResolver()
    {
    }
        
    public void Initialize(IContainer container)
    {
        _container = container;
    }

    public T Resolve<T>() where T: ViewModelBase
    {
        if (_container == null)
            throw new InvalidOperationException($"{nameof(ViewModelResolver)} not initialized yet.");

        return _container.Resolve<T>();
    }
        
    public T ResolveSingle<T>() where T: SingleInstanceViewModelBase
    {
        if (_container == null)
            throw new InvalidOperationException($"{nameof(ViewModelResolver)} not initialized yet.");

        return _container.Resolve<T>();
    }

    public object? Resolve(Type t)
        => _container?.Resolve(t);
}