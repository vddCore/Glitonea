﻿namespace Glitonea.Mvvm;

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Messaging;

public abstract class SingleInstanceViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void Subscribe<T>(Action<T> handler) where T : Message
        => Message.Subscribe(this, handler);

    protected void Unsubscribe<T>() where T : Message
        => Message.Unsubscribe<T>(this);
        
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}